using System;
using System.Collections.Generic;
using System.Linq;
using Coursework2021DB.DB;
using Microsoft.AspNetCore.Mvc;

namespace Coursework2021Api.Controllers.Users
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CourseDBContext context;

        public UsersController(CourseDBContext context)
        {
            this.context = context;
        }

        [HttpGet("/api/users")]
        public ActionResult<List<UserResponse>> Get()
        {
            var users = context.Users.Select(user => ResponseForModel(user))
                .ToList();
            return users;
        }

        [HttpGet("/api/user")]
        public ActionResult<UserResponse?> Get([FromQuery] string id)
        {
            var user = GetById(id);

            if (user == null) return BadRequest("No user found for id " + id);
            return ResponseForModel(user);
        }

        [HttpPost("/api/user")]
        public ActionResult<UserResponse> Post([FromBody] EditUserRequest request)
        {
            var user = GetById(request.Id);
            if (user == null) return BadRequest("Cannot find user for id " + request.Id);

            EditDBModel(user, request);

            context.Users.Update(user);
            SaveChanges(user);
            return ResponseForModel(user);
        }

        [HttpPut("/api/user")]
        public ActionResult<UserResponse> Put([FromBody] AddUserRequest request)
        {
            var user = CreateDBModel(request);
            context.Users.Add(user);
            SaveChanges(user);
            return ResponseForModel(user);
        }

        [HttpDelete("/api/user")]
        public ActionResult Delete([FromQuery] string id)
        {
            var user = GetById(id);
            if (user == null) return BadRequest("Cannot find user by given id");

            context.Users.Remove(user);
            context.SaveChanges();

            return Ok();
        }

        private User? GetById(string id)
        {
            var idInt = int.Parse(id);
            return context.Users.FirstOrDefault(u => u.Id == idInt);
        }

        private void SaveChanges(User model)
        {
            context.SaveChanges();
            context.Entry(model.UserLocation).Reference(ul => ul.Location).Load();
            context.Entry(model.UserLocation).Reference(ul => ul.User).Load();
            context.Entry(model).Reference(u => u.UserLocation).Load();
            context.Entry(model).Collection(u => u.RoomRentals).Load();
        }

        private static UserResponse ResponseForModel(User model)
        {
            return new()
            {
                Id = model.Id.ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Description = model.Description,
                LocationId = model.UserLocation?.LocationId.ToString(),
                LocationName = model.UserLocation?.Location?.Name,
                TimeCreated = model.TimeCreated,
                TimeUpdated = model.TimeUpdated
            };
        }

        private static User CreateDBModel(AddUserRequest request)
        {
            var userLocation = new UserLocation
            {
                LocationId = int.Parse(request.LocationId)
            };
            return new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Description = request.Description,
                UserLocation = userLocation,
                TimeCreated = DateTime.UtcNow,
                TimeUpdated = DateTime.UtcNow
            };
        }

        private static void EditDBModel(User model, EditUserRequest request)
        {
            var userLocation = new UserLocation
            {
                LocationId = int.Parse(request.LocationId)
            };
            model.FirstName = request.FirstName;
            model.LastName = request.LastName;
            model.Email = request.Email;
            model.Description = request.Description;
            model.UserLocation = userLocation;
            model.TimeUpdated = DateTime.UtcNow;
        }
    }
}
