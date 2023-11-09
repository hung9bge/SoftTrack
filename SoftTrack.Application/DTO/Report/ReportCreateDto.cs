using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Application.DTO.Report
{
    public class ReportCreateDto
    {
    
        public int AppId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        //public string Start_Date { get; set; }
        public string? End_Date { get; set; }
        public int Status { get; set; }
    }
}
