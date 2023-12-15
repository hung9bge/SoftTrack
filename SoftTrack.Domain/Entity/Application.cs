using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public partial class Application
    {
        public Application()
        {
            AssetApplications = new HashSet<AssetApplication>();
            Libraries = new HashSet<Library>();
            Reports = new HashSet<Report>();
        }

        public int AppId { get; set; }
        public int AccId { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Version { get; set; }
        public string Release { get; set; }
        public string Type { get; set; }
        public string Os { get; set; }
        public string Osversion { get; set; }
        public string Description { get; set; }
        public string Download { get; set; }
        public string Docs { get; set; }
        public string Language { get; set; }
        public string Db { get; set; }
        public int? Status { get; set; }

        public virtual Account Acc { get; set; }
        public virtual ICollection<AssetApplication> AssetApplications { get; set; }
        public virtual ICollection<Library> Libraries { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
