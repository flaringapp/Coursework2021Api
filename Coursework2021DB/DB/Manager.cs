using System;
using System.Collections.Generic;

#nullable disable

namespace Coursework2021DB.DB
{
    public partial class Manager
    {
        public Manager()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }

        public virtual ManagerLocation ManagerLocation { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
