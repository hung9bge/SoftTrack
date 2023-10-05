using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public partial class Role
    {
        public Role()
        {
            RoleAccounts = new HashSet<RoleAccount>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RoleAccount> RoleAccounts { get; set; }
    }
}
