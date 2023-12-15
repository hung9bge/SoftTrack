using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Manage.DTO
{ 
    public class LicenseUpdateDto
    {
   
        public int LicenseId { get; set; }
        public string LicenseKey { get; set; }
        public String? StartDate { get; set; }
        public int? Time { get; set; }
        public int Status { get; set; }

    }
}
