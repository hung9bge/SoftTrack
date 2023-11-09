using SoftTrack.Domain;

namespace SoftTrack.Software.DTO
{
    public class SoftwareDto
    {
        public int SoftwareId { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Version { get; set; }
        public string Release { get; set; }
        public string Os { get; set; }
        public int Status { get; set; }

    }
}
