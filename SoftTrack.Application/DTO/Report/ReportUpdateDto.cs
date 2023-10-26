using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Application.DTO.Report
{
    public class ReportUpdateDto
    {
        
        public int SoftwareId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string StartDate { get; set; }
        public string? EndDate { get; set; }
        public int Status { get; set; }
    }
}
