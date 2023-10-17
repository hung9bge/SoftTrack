using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public partial class RoleAccount
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int AccId { get; set; }

        public virtual Account Acc { get; set; }
        public virtual Role Role { get; set; }
    }
}
