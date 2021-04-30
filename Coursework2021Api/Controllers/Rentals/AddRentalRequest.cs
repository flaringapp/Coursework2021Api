using System;

namespace Coursework2021Api.Controllers.Rentals
{
    public class AddRentalRequest
    {
        public string RoomId { get; set; }
        public string UserId { get; set; }
    }
}