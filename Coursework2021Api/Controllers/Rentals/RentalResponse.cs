using System;

namespace Coursework2021Api.Controllers.Rentals
{
    public class RentalResponse
    {
        public string Id { get; set; }
        public string RoomId { get; set; }
        public string? RoomName { get; set; }
        public int? RoomPrice { get; set; }
        public string UserId { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DatePaidUntil { get; set; }
    }
}