using System;
using System.Collections.Generic;

#nullable disable

namespace Coursework2021DB.DB
{
    public partial class ManagerLocation
    {
        public int ManagerId { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual Manager LocationNavigation { get; set; }
    }
}
