using System;
using System.Collections.Generic;

namespace SoftTrack.Domain
{
    public partial class Device
    {
        public Device()
        {
            DeviceSoftwares = new HashSet<DeviceSoftware>();
        }

        public int DeviceId { get; set; }
        public string Name { get; set; }
        public string Cpu { get; set; }
        public string Gpu { get; set; }
        public int? Ram { get; set; }
        public int? Memory { get; set; }
        public string IpAddress { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public DateTime? LastSuccesfullScan { get; set; }
        public int Status { get; set; }

        public virtual ICollection<DeviceSoftware> DeviceSoftwares { get; set; }
    }
}
