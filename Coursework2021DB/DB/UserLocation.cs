using System;
using System.Collections.Generic;

#nullable disable

namespace Coursework2021DB.DB
{
    public partial class UserLocation
    {
        public int UserId { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual User User { get; set; }
    }
}
