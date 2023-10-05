using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public partial class Device
    {
        public Device()
        {
            Softwares = new HashSet<Software>();
        }

        public int DeviceId { get; set; }
        public int? AccId { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string LastSuccessfullScan { get; set; }
        public bool? Status { get; set; }

        public virtual Account Acc { get; set; }
        public virtual ICollection<Software> Softwares { get; set; }
    }
}
