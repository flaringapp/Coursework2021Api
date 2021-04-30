using System.Collections.Generic;
using System.Linq;
using Coursework2021Api.Utils;
using Coursework2021DB.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coursework2021Api.Controllers.Rooms
{
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly CourseDBContext context;

        public RoomsController(CourseDBContext context)
        {
            this.context = context;
        }

        [HttpGet("/api/rooms")]
        public ActionResult<List<RoomResponse>> GetList(
            [FromQuery] string? locationId
            )
        {
            IQueryable<Room> query = context.Rooms;
            if (locationId != null)
            {
                var locationIdInt = int.Parse(locationId);
                query = query.Where(room => room.LocationId == locationIdInt);
            }
            var rooms = query.Select(room => ResponseForModel(room))
                .ToList();
            return rooms;
        }

        [HttpGet("/api/room")]
        public ActionResult<RoomResponse?> Get([FromQuery] string id)
        {
            var room = GetById(id);

            if (room == null) return BadRequest("No room found for id " + id);
            return ResponseForModel(room);
        }

        [HttpPost("/api/room")]
        public ActionResult<RoomResponse> Post([FromBody] EditRoomRequest request)
        {
            var room = GetById(request.Id);
            if (room == null) return BadRequest("Cannot find room for id " + request.Id);

            EditDBModel(room, request);

            context.Rooms.Update(room);
            SaveChanges(room);
            return ResponseForModel(room);
        }

        [HttpPut("/api/room")]
        public ActionResult<RoomResponse> Put([FromBody] AddRoomRequest request)
        {
            var room = CreateDBModel(request);
            context.Rooms.Add(room);
            SaveChanges(room);
            return ResponseForModel(room);
        }

        [HttpDelete("/api/room")]
        public ActionResult Delete([FromQuery] string id)
        {
            var room = GetById(id);
            if (room == null) return BadRequest("Cannot find room by given id");

            context.Rooms.Remove(room);
            context.SaveChanges();

            return Ok();
        }

        private Room? GetById(string id)
        {
            var idInt = int.Parse(id);
            return context.Rooms.FirstOrDefault(r => r.Id == idInt);
        }

        private void SaveChanges(Room model)
        {
            context.SaveChanges();
            context.Entry(model).Reference(r => r.Location).Load();
            context.Entry(model).Collection(r => r.RoomRentals).Load();
        }

        private static RoomResponse ResponseForModel(Room room)
        {
            return new()
            {
                Id = room.Id.ToString(),
                LocationId = room.LocationId.ToString(),
                Name = room.Name,
                Description = room.Description,
                Type = room.Type,
                PlacesCount = room.PlacesCount,
                WindowCount = room.WindowCount,
                HasBoard = room.HasBoard.ToInt(),
                HasBalcony = room.HasBalcony.ToInt(),
                PlacePrice = room.PlacePrice,
                Area = room.Area
            };
        }

        private Room CreateDBModel(AddRoomRequest request)
        {
            var room = context.Rooms.CreateProxy();
            room.LocationId = int.Parse(request.LocationId);
            room.Name = request.Name;
            room.Description = request.Description;
            room.Type = request.Type;
            room.PlacesCount = request.PlacesCount;
            room.WindowCount = request.WindowCount;
            room.HasBoard = request.HasBoard.ToBool();
            room.HasBalcony = request.HasBalcony.ToBool();
            room.PlacePrice = request.PlacePrice;
            room.Area = request.Area;
            return room;
        }

        private static void EditDBModel(Room model, EditRoomRequest request)
        {
            model.Id = int.Parse(request.Id);
            model.LocationId = int.Parse(request.LocationId);
            model.Name = request.Name;
            model.Description = request.Description;
            model.Type = request.Type;
            model.PlacesCount = request.PlacesCount;
            model.WindowCount = request.WindowCount;
            model.HasBoard = request.HasBoard.ToBool();
            model.HasBalcony = request.HasBalcony.ToBool();
            model.PlacePrice = request.PlacePrice;
            model.Area = request.Area;
        }
    }
}
