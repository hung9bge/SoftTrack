using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public partial class License
    {
        public License()
        {
            DeviceSoftwares = new HashSet<DeviceSoftware>();
        }

        public int LicenseId { get; set; }
        public string LicenseKey { get; set; }
        public DateTime StartDate { get; set; }
        public int Time { get; set; }
        public int? Status { get; set; }

        public virtual ICollection<DeviceSoftware> DeviceSoftwares { get; set; }
    }
}
