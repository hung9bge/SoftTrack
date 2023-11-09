using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Application.DTO
{
    public class LicenseCreateDto
    {

        public int DeviceId { get; set; }
        public int SoftwareId { get; set; }
        public string LicenseKey { get; set; }
        public string? Start_Date { get; set; }
        public int Time { get; set; }
        public int? Status { get; set; }
    }
}
