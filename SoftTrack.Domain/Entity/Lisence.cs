using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public partial class Lisence
    {
        public Lisence()
        {
            DeviceSoftwares = new HashSet<DeviceSoftware>();
        }

        public int LisenceId { get; set; }
        public int SoftwareId { get; set; }
        public string LisenceKey { get; set; }
        public DateTime StartDate { get; set; }
        public int Time { get; set; }

        public virtual ICollection<DeviceSoftware> DeviceSoftwares { get; set; }
    }
}
