using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public partial class Account
    {
        public Account()
        {
            Softwares = new HashSet<Software>();
        }

        public int AccId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Software> Softwares { get; set; }
    }
}
