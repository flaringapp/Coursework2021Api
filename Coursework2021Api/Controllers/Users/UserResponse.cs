using System;

namespace Coursework2021Api.Controllers.Users
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Description { get; set; }
        public string? LocationId { get; set; }
        public string? LocationName { get; set; }
        public string TimeCreated { get; set; }
        public string TimeUpdated { get; set; }
    }
}