using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Application.DTO
{
    public class LisenceDto
    {
        public int LisenceId { get; set; }
        public int SoftwareId { get; set; }
        public string LisenceKey { get; set; }
        public String StartDate { get; set; }
        public int Time { get; set; }

    }
}
