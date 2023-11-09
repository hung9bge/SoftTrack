using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Software.DTO
{
    public class LicenseUpdateDto
    {
        public string LicenseKey { get; set; }
        public string Start_Date { get; set; }
        public int Time { get; set; }
        public int? Status { get; set; }
    }
}
