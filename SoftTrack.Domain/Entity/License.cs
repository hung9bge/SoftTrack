using System;
using System.Collections.Generic;

namespace SoftTrack.Domain.Entity
{
    public partial class License
    {
        public License()
        {
            AssetSoftwares = new HashSet<AssetSoftware>();
        }

        public int LicenseId { get; set; }
        public string LicenseKey { get; set; }
        public DateTime? StartDate { get; set; }
        public int? Time { get; set; }
        public int Status { get; set; }

        public virtual ICollection<AssetSoftware> AssetSoftwares { get; set; }
    }
}
