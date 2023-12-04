using Microsoft.AspNetCore.Mvc;
using SoftTrack.API.Models;

namespace SoftTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : Controller
    {
        public static IWebHostEnvironment _webHostEnvironment;
        public UploadFileController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        public async Task<string> Post([FromForm] FileUpload fileUpload)
        {
            try
            {
                if (fileUpload.files.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\upload\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (FileStream fs = System.IO.File.Create(path + fileUpload.files.FileName))
                    {
                        fileUpload.files.CopyTo(fs);
                        fs.Flush();
                        return "Upload Done.";
                    }
                }
                else
                {
                    return "Failed.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        } 
        [HttpGet("{fileName}")]
        public async Task<IActionResult> Get([FromRoute] string fileName)
        {
            string path = _webHostEnvironment.WebRootPath + "\\upload\\";
            var filePath = path + fileName + ".txt";
            if (System.IO.File.Exists(filePath))
            {
                byte[] buf = System.IO.File.ReadAllBytes(filePath);
                return File(buf, "info/txt");
            }
            return null;
        }
    }
}
