using SoftTrack.Domain;
using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public class Account
    {
        public Account()
        {
            Applications = new HashSet<Application>();
        }

        public int AccId { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}
