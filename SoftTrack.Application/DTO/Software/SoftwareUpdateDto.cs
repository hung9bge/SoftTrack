using SoftTrack.Domain;

namespace SoftTrack.Application.DTO
{
    public class SoftwareUpdateDto
    {
       
        public int AccId { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Version { get; set; }
        public string Release { get; set; }
        public string Type { get; set; }
        public string Os { get; set; }
        public string Description { get; set; }
        public string Download { get; set; }
        public string Docs { get; set; }
        public int Status { get; set; }
       
        //public virtual Device? Device { get; set; }
        //public virtual ICollection<Issue> Issues { get; set; }
    }
}
