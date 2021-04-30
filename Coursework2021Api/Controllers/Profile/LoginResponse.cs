using System;

namespace Coursework2021Api.Controllers.Profile
{
    public class LoginResponse
    {
        public String Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Token { get; set; }
        public String UserType { get; set; }
        public String? LocationId { get; set; }
    }
}