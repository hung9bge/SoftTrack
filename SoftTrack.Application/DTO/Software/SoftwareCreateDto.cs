using SoftTrack.Domain;

namespace SoftTrack.Application.DTO
{
    public class SoftwareCreateDto
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Version { get; set; }
        public string Release { get; set; }
        public string Os { get; set; }
        public int Status { get; set; }
    }
}
