using System;
using System.Collections.Generic;

#nullable disable

namespace Coursework2021DB.DB
{
    public partial class Room
    {
        public Room()
        {
            RoomRentals = new HashSet<RoomRental>();
        }

        public int Id { get; set; }
        public int LocationId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float? Area { get; set; }
        public short? WindowCount { get; set; }
        public bool? HasBalcony { get; set; }
        public bool? HasBoard { get; set; }
        public short PlacesCount { get; set; }
        public int PlacePrice { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<RoomRental> RoomRentals { get; set; }
    }
}
