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
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Version { get; set; }
        public string Release { get; set; }
        public string Type { get; set; }
        public string Os { get; set; }
        public String? InstallDate { get; set; }
        public string LicenseKey { get; set; }
        public String? Start_Date { get; set; }
        public int? Time { get; set; }
        public int Status_License { get; set; }
        public int Status_Software { get; set; }
        public int Status_AssetSoftware { get; set; }

    }
}
