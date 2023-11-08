using System;
using System.Collections.Generic;

namespace SoftTrack.Domain.Entity
{
    public partial class Software
    {
        public Software()
        {
            AssetSoftwares = new HashSet<AssetSoftware>();
        }

        public int SoftwareId { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Version { get; set; }
        public string Release { get; set; }
        public string Os { get; set; }
        public int Status { get; set; }

        public virtual ICollection<AssetSoftware> AssetSoftwares { get; set; }
    }
}
