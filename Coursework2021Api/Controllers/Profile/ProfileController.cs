using System.Linq;
using Coursework2021Api.Auth;
using Coursework2021DB.DB;
using Microsoft.AspNetCore.Mvc;

namespace Coursework2021Api.Controllers.Profile
{
    [ApiController]
    public class ProfileController : ControllerBase
    {

        private const int ADMIN_ID = 5810472;
        private const string ADMIN_FIRST_NAME = "Andrew";
        private const string ADMIN_LAST_NAME = "Shpek";
        private const string ADMIN_EMAIL = "ashpek@testmail.com";
        private const string ADMIN_PASSWORD = "Aa123456";

        private readonly CourseDBContext context;

        public ProfileController(CourseDBContext context)
        {
            this.context = context;
        }

        [HttpPost("/api/login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
        {
            if (request.Email == ADMIN_EMAIL)
            {
                if (request.Password != ADMIN_PASSWORD) return BadRequest("Wrong email or password");
                return GenerateAdminResponse();
            }

            var manager =
                context.Managers.FirstOrDefault(m => m.Email == request.Email && m.Password == request.Password);
            if (manager == null) return BadRequest("Wrong email or password");

            var token = AuthUtils.GenerateToken(manager.Id);
            var locationId = manager.ManagerLocation?.LocationId;

            var response = new LoginResponse
            {
                Id = manager.Id.ToString(),
                FirstName = manager.FirstName,
                LastName = manager.LastName,
                Email = manager.Email,
                Token = token,
                UserType = "manager",
                LocationId = locationId?.ToString()
            };

            return response;
        }

        private LoginResponse GenerateAdminResponse()
        {
            return new()
            {
                Id = ADMIN_ID.ToString(),
                FirstName = ADMIN_FIRST_NAME,
                LastName = ADMIN_LAST_NAME,
                Email = "ashpek@testmail.com",
                Token = AuthUtils.GenerateToken(ADMIN_ID),
                UserType = "admin",
                LocationId = null
            };
        }
    }
}