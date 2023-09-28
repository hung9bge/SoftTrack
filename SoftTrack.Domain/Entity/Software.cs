namespace SoftTrack.Domain
{
    public partial class Software
    {
        public int SoftwareId { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Publisher { get; set; }
        public string Type { get; set; }
        public DateTime? InstallDate { get; set; }
        public string Status { get; set; }
    }
}
