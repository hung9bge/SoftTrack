using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public partial class Authorization
    {
        public int AuthorizationId { get; set; }
        public int SoftwareId { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string AuthorizationKey { get; set; }
        public DateTime StartDate { get; set; }
        public int Time { get; set; }
        public int Status { get; set; }

        public virtual Software Software { get; set; }
    }
}
