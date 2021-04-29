namespace Coursework2021Api.Controllers.Rooms
{
    public class RoomResponse
    {
        public string Id { get; set; }
        public string LocationId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
        public short PlacesCount { get; set; }
        public short? WindowCount { get; set; }
        public bool? HasBoard { get; set; }
        public bool? HasBalcony { get; set; }
        public int PlacePrice { get; set; }
        public float? Area { get; set; }
    }
}