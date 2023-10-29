using Microsoft.AspNetCore.Mvc;
using SoftTrack.API.Models;
using SoftTrack.Application.DTO;
using SoftTrack.Application.Interface;
using SoftTrack.Domain;

namespace SoftTrack.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UploadFileController : Controller
    {
        public static IWebHostEnvironment _webHostEnvironment;
        private readonly IDeviceService _DeviceService;
        public UploadFileController(IWebHostEnvironment webHostEnvironment, IDeviceService DeviceService)
        {
            _webHostEnvironment = webHostEnvironment;
            _DeviceService = DeviceService;
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
                    string filePath = path + fileUpload.files.FileName;
                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        fileUpload.files.CopyTo(fs);
                        fs.Flush();
                    }
                    string[] systemInformation = new string[10];
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
                        var sysInfo = new DeviceCreateDto
                        {
                            Name = systemInformation[0],
                            Cpu = systemInformation[1],
                            Ram = (int)Math.Round(Convert.ToDouble(systemInformation[2])),
                            Memory = (int)Math.Round(Convert.ToDouble(systemInformation[3])),
                            IpAddress = systemInformation[4],
                            Manufacturer = systemInformation[5],
                            Model = systemInformation[6],
                            SerialNumber = systemInformation[7],
                            LastSuccesfullScan = systemInformation[8]
                        };

                        if (sysInfo != null)
                        {
                            await _DeviceService.CreateDeviceAsync(sysInfo);
                        }
                        else
                        {
                            return "Data is null.";
                        }    
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
