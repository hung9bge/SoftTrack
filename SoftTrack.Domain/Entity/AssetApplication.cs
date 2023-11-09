using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public partial class AssetApplication
    {
        public int AssetId { get; set; }
        public int AppId { get; set; }
        public DateTime? InstallDate { get; set; }
        public int Status { get; set; }

        public virtual Application App { get; set; }
        public virtual Asset Asset { get; set; }
    }
}
