using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Software.DTO
{
    public class LicenseDto
    {
        public int AssetId { get; set; }
        public int SoftwareId { get; set; }
        public String? InstallDate { get; set; }
        public int Status_AssetSoftware { get; set; }
        public string LicenseKey { get; set; }
        public String? Start_Date { get; set; }
        public int? Time { get; set; }
        public int Status_License { get; set; }

    }
}
