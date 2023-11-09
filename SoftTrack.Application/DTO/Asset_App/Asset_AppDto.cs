using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Software.DTO
{
    public class AssetApplicationDTO
    {
        public int AssetId { get; set; }
        public int AppId { get; set; }
        public String? InstallDate { get; set; }
        public int Status { get; set; }
    }
}
