using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Manage.DTO
{
    public class ReportModel
    {
        //public int ReportId { get; set; }

        public int AppId { get; set; }
        public int AccId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Start_Date { get; set; }
        public string? End_Date { get; set; }
        public int Status { get; set; }
        public IFormFileCollection? Images { get; set; }
    }
}
