using System;
using System.Collections.Generic;

#nullable disable

namespace Coursework2021DB.DB
{
    public partial class User
    {
        public User()
        {
            RoomRentals = new HashSet<RoomRental>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }

        public virtual UserLocation UserLocation { get; set; }
        public virtual ICollection<RoomRental> RoomRentals { get; set; }
    }
}
