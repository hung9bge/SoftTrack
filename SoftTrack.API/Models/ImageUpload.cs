namespace SoftTrack.API.Models
{
    public class ImageUpload
    {
        public int ReportId { get; set; }
        public string Image1 { get; set; }
        public IFormFile Images { get; set; }
    }
}
