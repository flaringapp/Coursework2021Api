using System;
using System.Collections.Generic;

#nullable disable

namespace Coursework2021DB.DB
{
    public partial class RoomRental
    {
        public RoomRental()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DatePaidUntil { get; set; }

        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
