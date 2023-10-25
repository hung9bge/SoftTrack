using SoftTrack.Domain;

namespace SoftTrack.Application.DTO
{
    public class DeviceUpdateDto
    {
        public string Name { get; set; }
        public string Cpu { get; set; }
        public string Gpu { get; set; }
        public int? Ram { get; set; }
        public int? Memory { get; set; }
        public string IpAddress { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public String? LastSuccesfullScan { get; set; }
        public int Status { get; set; }

    }
}
