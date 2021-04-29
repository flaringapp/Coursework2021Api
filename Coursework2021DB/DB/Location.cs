using System;
using System.Collections.Generic;

#nullable disable

namespace Coursework2021DB.DB
{
    public partial class Location
    {
        public Location()
        {
            ManagerLocations = new HashSet<ManagerLocation>();
            Rooms = new HashSet<Room>();
            UserLocations = new HashSet<UserLocation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float GeoLat { get; set; }
        public float GeoLon { get; set; }
        public float Area { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }

        public virtual ICollection<ManagerLocation> ManagerLocations { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<UserLocation> UserLocations { get; set; }
    }
}
