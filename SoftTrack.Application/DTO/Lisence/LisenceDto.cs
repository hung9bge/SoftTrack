using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Application.DTO
{
    public class LicenseDto
    {
        public int LicenseId { get; set; }
        public string LicenseKey { get; set; }
        public string StartDate { get; set; }
        public int Time { get; set; }
        public int? Status { get; set; }
        public int SoftwareId { get; set; }

    }
}
