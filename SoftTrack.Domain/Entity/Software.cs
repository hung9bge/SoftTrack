using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public partial class Software
    {
        public Software()
        {
            Authorizations = new HashSet<Authorization>();
            DeviceSoftwares = new HashSet<DeviceSoftware>();
            Reports = new HashSet<Report>();
        }

        public int SoftwareId { get; set; }
        public int AccId { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Version { get; set; }
        public string Release { get; set; }
        public string Type { get; set; }
        public string Os { get; set; }
        public string Description { get; set; }
        public string Download { get; set; }
        public string Docs { get; set; }
        public int Status { get; set; }

        public virtual Account Acc { get; set; }
        public virtual ICollection<Authorization> Authorizations { get; set; }
        public virtual ICollection<DeviceSoftware> DeviceSoftwares { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
