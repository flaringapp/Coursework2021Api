using System;
using System.Collections.Generic;
using System.Linq;
using Coursework2021DB.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Coursework2021Api.Controllers.Rentals
{
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly CourseDBContext context;

        public RentalsController(CourseDBContext context)
        {
            this.context = context;
        }

        [HttpGet("/api/rentals")]
        public ActionResult<List<RentalResponse>> GetList(
            [FromQuery] string? roomId,
            [FromQuery] string? userId)
        {
            IQueryable<RoomRental> query = context.RoomRentals;
            if (roomId != null)
            {
                var roomIdInt = int.Parse(roomId);
                query = query.Where(rental => rental.RoomId == roomIdInt);
            }

            if (userId != null)
            {
                var userIdInt = int.Parse(userId);
                query = query.Where(rental => rental.UserId == userIdInt);
            }
            var rentals = query.Select(rental => ResponseForModel(rental))
                .ToList();
            return rentals;
        }

        [HttpGet("/api/rental")]
        public ActionResult<RentalResponse?> Get([FromQuery] string id)
        {
            var rental = GetById(id);
            if (rental == null) return BadRequest("No rental found for id " + id);
            return ResponseForModel(rental);
        }

        [HttpPut("/api/rental")]
        public ActionResult<RentalResponse> Put([FromBody] AddRentalRequest request)
        {
            var rental = CreateDBModel(request);
            context.RoomRentals.Add(rental);
            SaveChanges(rental);
            return ResponseForModel(rental);
        }

        [HttpDelete("/api/rental")]
        public ActionResult Delete([FromQuery] string id)
        {
            var rental = GetById(id);
            if (rental == null) return BadRequest("Cannot find rental by given id");

            context.RoomRentals.Remove(rental);
            context.SaveChanges();

            return Ok();
        }

        private RoomRental? GetById(string id)
        {
            var idInt = int.Parse(id);
            return context.RoomRentals.FirstOrDefault(rr => rr.Id == idInt);
        }

        private void SaveChanges(RoomRental model)
        {
            context.SaveChanges();
            context.Entry(model).Reference(rr => rr.Room).Load();
            context.Entry(model).Collection(rr => rr.Transactions).Load();
        }

        private static RentalResponse ResponseForModel(RoomRental model)
        {
            return new()
            {
                Id = model.Id.ToString(),
                UserId = model.UserId.ToString(),
                UserFirstName = model.User?.FirstName,
                UserLastName = model.User?.LastName,
                RoomId = model.RoomId.ToString(),
                RoomName = model.Room?.Name,
                RoomPrice = model.Room?.PlacePrice,
                DateStart = model.DateStart,
                DatePaidUntil = model.DatePaidUntil
            };
        }

        private RoomRental CreateDBModel(AddRentalRequest request)
        {
            var rental = context.RoomRentals.CreateProxy();
            rental.UserId = int.Parse(request.UserId);
            rental.RoomId = int.Parse(request.RoomId);;
            rental.DateStart = DateTime.UtcNow;
            rental.DatePaidUntil = null;
            return rental;
        }
    }
}
