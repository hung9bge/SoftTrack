using Microsoft.AspNetCore.Mvc;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using System.Globalization;

namespace SoftTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : Controller
    {
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly soft_track5Context _context;
        public UploadFileController(IWebHostEnvironment webHostEnvironment, soft_track5Context context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
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
                    string filePath = path + Guid.NewGuid().ToString() + "_" + fileUpload.files.FileName;
                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        fileUpload.files.CopyTo(fs);
                        fs.Flush();
                    }
                    string[] systemInformation = new string[15];
                    try
                    {
                        using (StreamReader sr = new StreamReader(filePath))
                        {
                            string line;
                            int it = 0;
                            while ((line = sr.ReadLine()) != null)
                            {
                                string[] parts = line.Split(':');
                                if (parts.Length == 2)
                                {
                                    systemInformation[it] = parts[1].Trim();
                                }
                                it++;
                            }
                        }
                        var sysInfo = new Asset
                        {
                            Name = systemInformation[0],
                            Cpu = systemInformation[1],
                            Gpu = systemInformation[2],
                            Ram = systemInformation[3],
                            Memory = systemInformation[4],
                            Os = systemInformation[5],
                            Version = "",
                            IpAddress = systemInformation[6],
                            Bandwidth = "",
                            Manufacturer = systemInformation[7],
                            Model = systemInformation[8],
                            SerialNumber = systemInformation[9],                            
                        };

                        if (DateTime.TryParseExact(systemInformation[10], "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                        {
                            sysInfo.LastSuccesfullScan = parsedDate;
                        }

                        if (sysInfo.Name == null)
                        {
                            return "Data is invalid.";
                        }
                        
                        _context.Assets.Add(sysInfo);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                    return "Upload Done.";
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
