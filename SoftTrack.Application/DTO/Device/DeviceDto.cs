using SoftTrack.Domain;

namespace SoftTrack.Application.DTO
{
    public class DeviceDto
    {
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

        //public virtual Account? Acc { get; set; }
        public virtual ICollection<SoftwaresDeviceDto> Softwares { get; set; }
    }
    public class SoftwaresDeviceDto
    {
        public int SoftwareId { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Publisher { get; set; }
        public string Type { get; set; }
        public DateTime InstallDate { get; set; }
        public bool? Status { get; set; }

    }
}
