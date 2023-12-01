namespace SoftTrack.API.Models
{
    public class ReportModel
    {
        //public int ReportId { get; set; }
        public int AppId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Start_Date { get; set; }
        public string? End_Date { get; set; }
        public int Status { get; set; }
        public IFormFileCollection? Images { get; set; }
    }
}
