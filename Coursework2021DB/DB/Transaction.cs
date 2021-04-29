using System;
using System.Collections.Generic;

#nullable disable

namespace Coursework2021DB.DB
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public int ManagerId { get; set; }
        public int RentId { get; set; }
        public int Amount { get; set; }
        public DateTime? DatePaidFrom { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime DatePaidTo { get; set; }

        public virtual Manager Manager { get; set; }
        public virtual RoomRental Rent { get; set; }
    }
}
