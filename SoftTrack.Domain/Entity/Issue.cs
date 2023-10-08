using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public partial class Issue
    {
        public int IssueId { get; set; }
        public int? SoftwareId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool? Status { get; set; }

        public virtual Software Software { get; set; }
    }
}
