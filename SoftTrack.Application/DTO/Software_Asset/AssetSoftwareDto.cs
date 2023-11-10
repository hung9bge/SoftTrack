using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Manage.DTO
{
    public class AssetSoftwareDto
    {
        public int AssetId { get; set; }
        public int SoftwareId { get; set; }
        public String? InstallDate { get; set; }
        public int Status { get; set; }

    }
}
