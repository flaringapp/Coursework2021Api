using System;

namespace Coursework2021Api.Controllers.Transactions
{
    public class TransactionResponse
    {
        public string Id { get; set; }
        public string? RentalId { get; set; }
        public string? UserId { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public string? RoomId { get; set; }
        public string? RoomName { get; set; }
        public string? RoomType { get; set; }
        public string? ManagerId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int Amount { get; set; }
        public DateTime TimeCreated { get; set; }
    }
}