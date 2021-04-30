using System;

namespace Coursework2021Api.Controllers.Managers
{
    public class AddManagerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Description { get; set; }
        public string LocationId { get; set; }
    }
}