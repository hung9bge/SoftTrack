using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public partial class Report
    {
        public Report()
        {
            Images = new HashSet<Image>();
        }

        public int ReportId { get; set; }
        public int AppId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Status { get; set; }

        public virtual Application App { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
