using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public partial class Account
    {
        public Account()
        {
            Devices = new HashSet<Device>();
            RoleAccounts = new HashSet<RoleAccount>();
        }

        public int AccId { get; set; }
        public string Account1 { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string ?PhoneNumber { get; set; }

        public virtual ICollection<Device>? Devices { get; set; }
        public virtual ICollection<RoleAccount> RoleAccounts { get; set; }
    }
}
