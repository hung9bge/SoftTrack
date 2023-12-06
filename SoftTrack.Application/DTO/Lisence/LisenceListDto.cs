using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Manage.DTO
{
    public class LisenceListDto
    {
        public int AssetId { get; set; }
        public int SoftwareId { get; set; }
        public int LicenseId { get; set; }
        public string LicenseKey { get; set; }
        public String? Start_Date { get; set; }
        public int? Time { get; set; }
        public int Status { get; set; }
    }
}
