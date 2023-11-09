using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Software.DTO
{
    public class LicenseDto
    {
        public int LicenseId { get; set; }
        public string LicenseKey { get; set; }
        public DateTime? Start_Date { get; set; }
        public int? Time { get; set; }
        public int Status { get; set; }

    }
}
