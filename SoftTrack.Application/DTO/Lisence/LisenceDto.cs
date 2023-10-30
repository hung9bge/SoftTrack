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
        public string LisenceKey { get; set; }
        public string StartDate { get; set; }
        public int Time { get; set; }
        public int? Status { get; set; }

    }
}
