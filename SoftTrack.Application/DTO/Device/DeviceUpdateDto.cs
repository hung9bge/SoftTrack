using SoftTrack.Domain;

namespace SoftTrack.Application.DTO
{
    public class DeviceUpdateDto
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

        public virtual Account Acc { get; set; }
        public virtual ICollection<Software> Softwares { get; set; }
    }
}
