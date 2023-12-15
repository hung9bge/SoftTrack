using SoftTrack.Domain;

namespace SoftTrack.Manage.DTO
{
    public class SoftwareListDto
    {
        public int SoftwareId { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Version { get; set; }
        public string Release { get; set; }
        public string Type { get; set; }
        public string Os { get; set; }
        public String? InstallDate { get; set; }
        public int AssetSoftwareStatus { get; set; }

    }
}
