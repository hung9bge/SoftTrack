using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public partial class DeviceSoftware
    {
        public int DeviceId { get; set; }
        public int SoftwareId { get; set; }
        public int? LicenseId { get; set; }
        public DateTime InstallDate { get; set; }
        public int Status { get; set; }

        public virtual Device Device { get; set; }
        public virtual License License { get; set; }
        public virtual Software Software { get; set; }
    }
}
