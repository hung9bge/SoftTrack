using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTrack.Manage.DTO
{
    public class AssetUpdateDto
    {
        public string Name { get; set; }
        public string Cpu { get; set; }
        public string Gpu { get; set; }
        public string Ram { get; set; }
        public string Memory { get; set; }
        public string Os { get; set; }
        public string Version { get; set; }
        public string IpAddress { get; set; }
        public string Bandwidth { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public String? LastSuccesfullScan { get; set; }
        public int Status { get; set; }
    }
}
