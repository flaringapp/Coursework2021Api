namespace Coursework2021Api.Controllers.Locations
{
    public class LocationRequest
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Lat { get; set; }

        public float Lon { get; set; }

        public string Address { get; set; }

        public float Area { get; set; }
    }
}