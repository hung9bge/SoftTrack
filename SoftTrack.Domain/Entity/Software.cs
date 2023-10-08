using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public partial class Software
    {
        public Software()
        {
            Issues = new HashSet<Issue>();
        }

        public int SoftwareId { get; set; }
        public int? DeviceId { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Publisher { get; set; }
        public string Type { get; set; }
        public DateTime InstallDate { get; set; }
        public bool? Status { get; set; }

        public virtual Device? Device { get; set; }
        public virtual ICollection<Issue> Issues { get; set; }
    }
}
