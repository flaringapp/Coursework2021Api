using System.Collections.Generic;
using System.Linq;
using Coursework2021DB.DB;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<List<RoomResponse>> Get()
        {
            var rooms = context.Rooms.Select(room => ResponseForModel(room))
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
                HasBoard = room.HasBoard,
                HasBalcony = room.HasBalcony,
                PlacePrice = room.PlacePrice,
                Area = room.Area
            };
        }

        private static Room CreateDBModel(AddRoomRequest request)
        {
            return new()
            {
                LocationId = int.Parse(request.LocationId),
                Name = request.Name,
                Description = request.Description,
                Type = request.Type,
                PlacesCount = request.PlacesCount,
                WindowCount = request.WindowCount,
                HasBoard = request.HasBoard,
                HasBalcony = request.HasBalcony,
                PlacePrice = request.PlacePrice,
                Area = request.Area
            };
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
            model.HasBoard = request.HasBoard;
            model.HasBalcony = request.HasBalcony;
            model.PlacePrice = request.PlacePrice;
            model.Area = request.Area;
        }
    }
}
