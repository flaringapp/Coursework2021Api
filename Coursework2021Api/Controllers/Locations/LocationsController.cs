using System.Collections.Generic;
using System.Linq;
using Coursework2021DB.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var locations = context.Locations.AsNoTracking().Select(location => ResponseForModel(location))
                .ToList();
            return locations;
        }

        [HttpGet("/api/location")]
        public ActionResult<LocationResponse?> Get([FromQuery] string id)
        {
            var location = GetById(id);

            if (location == null) return BadRequest("No location found for id " + id);
            return ResponseForModel(location);
        }

        [HttpPost("/api/location")]
        public ActionResult<LocationResponse> Post([FromBody] EditLocationRequest request)
        {
            var location = GetById(request.Id);
            if (location == null) return BadRequest("Cannot find location for id " + request.Id);

            EditDBModel(location, request);

            context.Locations.Update(location);
            context.SaveChanges();
            // context.Entry(location).Reference().Load();
            return ResponseForModel(location);
        }

        [HttpPut("/api/location")]
        public ActionResult<LocationResponse> Put([FromBody] AddLocationRequest request)
        {
            var location = CreateDBModel(request);
            context.Locations.Add(location);
            context.SaveChanges();
            // context.Entry(location).Reference().Load();
            return ResponseForModel(location);
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

        private Location? GetById(string id)
        {
            var idInt = int.Parse(id);
            return context.Locations.AsNoTracking().FirstOrDefault(loc => loc.Id == idInt);
        }

        private static LocationResponse ResponseForModel(Location location)
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

        private static Location CreateDBModel(AddLocationRequest request)
        {
            return new()
            {
                Name = request.Name,
                Description = request.Description,
                GeoLat = request.Lat,
                GeoLon = request.Lon,
                Address = request.Address,
                Area = request.Area
            };
        }

        private static void EditDBModel(Location model, EditLocationRequest request)
        {
            model.Id = int.Parse(request.Id);
            model.Name = request.Name;
            model.Description = request.Description;
            model.GeoLat = request.Lat;
            model.GeoLon = request.Lon;
            model.Address = request.Address;
            model.Area = request.Area;
        }
    }
}
