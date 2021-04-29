using System.Collections.Generic;
using System.Linq;
using Coursework2021DB.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coursework2021Api.Controllers.Locations
{
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly CourseDBContext context;

        public LocationsController(CourseDBContext context)
        {
            this.context = context;
        }

        [HttpGet("/api/locations")]
        public ActionResult<List<LocationResponse>> Get()
        {
            var locations = context.Locations.Select(location => ResponseForLocation(location))
                .ToList();
            return locations;
        }

        [HttpGet("/api/location")]
        public ActionResult<LocationResponse?> Get([FromQuery] string id)
        {
            var idInt = int.Parse(id);
            var location = context.Locations
                .FirstOrDefault(loc => loc.Id == idInt);

            if (location == null) return BadRequest("No location found for id");
            return ResponseForLocation(location);
        }

        [HttpPost("/api/location")]
        public ActionResult<LocationResponse> Post([FromBody] LocationRequest request)
        {
            var location = CreateLocation(request);
            context.Locations.Update(location);
            context.SaveChanges();
            return ResponseForLocation(location);
        }

        [HttpPut("/api/location")]
        public ActionResult<LocationResponse> Put([FromBody] LocationRequest request)
        {
            var location = CreateLocation(request, true);
            context.Locations.Add(location);
            context.SaveChanges();
            return ResponseForLocation(location);
        }

        [HttpDelete("/api/location")]
        public ActionResult Delete([FromQuery] string id)
        {
            var idInt = int.Parse(id);
            var location = context.Locations.FirstOrDefault(loc => loc.Id == idInt);
            if (location == null) return BadRequest("Cannot find location by given id");

            context.Locations.Remove(location);
            context.SaveChanges();

            return Ok();
        }

        private static LocationResponse ResponseForLocation(Location location)
        {
            return new()
            {
                Id = location.Id.ToString(),
                Name = location.Name,
                Description = location.Description,
                Lat = location.GeoLat,
                Lon = location.GeoLon,
                Address = location.Address,
                Area = location.Area,
            };
        }

        private static Location CreateLocation(LocationRequest request, bool withId = false)
        {
            var location = new Location
            {
                Name = request.Name,
                Description = request.Description,
                GeoLat = request.Lat,
                GeoLon = request.Lon,
                Address = request.Address,
                Area = request.Area,
            };
            if (withId)
            {
                location.Id = int.Parse(request.Id);
            }
            return location;
        }
    }
}
