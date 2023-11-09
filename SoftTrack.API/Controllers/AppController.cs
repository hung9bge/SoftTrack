using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Domain;
using SoftTrack.Software.DTO;

namespace SoftTrack.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppController : Controller
    {
        private readonly soft_track4Context _context;
        private readonly IConfiguration _configuration;
        public AppController(IConfiguration configuration, soft_track4Context context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet("ListApps")]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> ListAllAppsAsync()
        {
            var appDtos = await _context.Applications
                .Select(app => new ApplicationDto
                {
                    AppId = app.AppId,
                    AccId = app.AccId,
                    Name = app.Name,
                    Publisher = app.Publisher,
                    Version = app.Version,
                    Release = app.Release,
                    Type = app.Type,
                    Os = app.Os,
                    Osversion = app.Osversion,
                    Description = app.Description,
                    Download = app.Download,
                    Docs = app.Docs,
                    Language = app.Language,
                    Db = app.Db
                })
                .ToListAsync();

            return appDtos;
        }


        [HttpPost("CreateApp")]
        public async Task<IActionResult> CreateAppAsync([FromBody] ApplicationCreateDto appCreateDto)
        {
            if (ModelState.IsValid) // Kiểm tra tính hợp lệ của dữ liệu đầu vào
            {
                // Tạo một đối tượng Application từ dữ liệu được chuyển đến từ đối tượng ApplicationCreateDto
                var application = new Application
                {
                    Name = appCreateDto.Name,
                    Publisher = appCreateDto.Publisher,
                    Version = appCreateDto.Version,
                    Release = appCreateDto.Release,
                    Type = appCreateDto.Type,
                    Os = appCreateDto.Os,
                    Osversion = appCreateDto.Osversion,
                    Description = appCreateDto.Description,
                    Download = appCreateDto.Download,
                    Docs = appCreateDto.Docs,
                    Language = appCreateDto.Language,
                    Db = appCreateDto.Db
                    // Gán các giá trị khác tương ứng từ appCreateDto
                };

                _context.Applications.Add(application);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetApplication", new { id = application.AppId }, application);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("UpdateApplication/{id}")]
        public async Task<IActionResult> UpdateApplicationAsync(int id, [FromBody] ApplicationUpdateDto updatedAppDto)
        {
            // Tìm đối tượng Application trong cơ sở dữ liệu bằng id
            var updatedApp = await _context.Applications.FindAsync(id);

            if (updatedApp == null)
            {
                // Không tìm thấy ứng dụng, xử lý lỗi ở đây (ví dụ: trả về lỗi hoặc thông báo)
                return NotFound();
            }

            // Kiểm tra và cập nhật các trường không null và không phải là "string"
            updatedApp.Name = (updatedAppDto.Name != null && updatedAppDto.Name != "string") ? updatedAppDto.Name : updatedApp.Name;
            updatedApp.Publisher = (updatedAppDto.Publisher != null && updatedAppDto.Publisher != "string") ? updatedAppDto.Publisher : updatedApp.Publisher;
            updatedApp.Version = (updatedAppDto.Version != null && updatedAppDto.Version != "string") ? updatedAppDto.Version : updatedApp.Version;
            updatedApp.Release = (updatedAppDto.Release != null && updatedAppDto.Release != "string") ? updatedAppDto.Release : updatedApp.Release;
            updatedApp.Type = (updatedAppDto.Type != null && updatedAppDto.Type != "string") ? updatedAppDto.Type : updatedApp.Type;
            updatedApp.Os = (updatedAppDto.Os != null && updatedAppDto.Os != "string") ? updatedAppDto.Os : updatedApp.Os;
            updatedApp.Osversion = (updatedAppDto.Osversion != null && updatedAppDto.Osversion != "string") ? updatedAppDto.Osversion : updatedApp.Osversion;
            updatedApp.Description = (updatedAppDto.Description != null && updatedAppDto.Description != "string") ? updatedAppDto.Description : updatedApp.Description;
            updatedApp.Download = (updatedAppDto.Download != null && updatedAppDto.Download != "string") ? updatedAppDto.Download : updatedApp.Download;
            updatedApp.Docs = (updatedAppDto.Docs != null && updatedAppDto.Docs != "string") ? updatedAppDto.Docs : updatedApp.Docs;
            updatedApp.Language = (updatedAppDto.Language != null && updatedAppDto.Language != "string") ? updatedAppDto.Language : updatedApp.Language;
            updatedApp.Db = (updatedAppDto.Db != null && updatedAppDto.Db != "string") ? updatedAppDto.Db : updatedApp.Db;

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            return Ok(updatedApp);
        }


        [HttpDelete("DeleteAppWith_key")]
        public async Task<IActionResult> DeleteAppAsync(int Appid)
        {
            var deleteApp = await _context.Applications.FindAsync(Appid);

            if (deleteApp != null)
            {
                _context.Applications.Remove(deleteApp);
                await _context.SaveChangesAsync();
            }
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpGet("list_App_by_user/{key}")]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetAppForAccountAsync(int key)
        {
            // Assuming 'key' is AccId
            var applications = await _context.Applications
                .Where(app => app.AccId == key)
                .Select(app => new ApplicationDto // Assuming you have a DTO for Application
                {
                    // Map Application properties to ApplicationDto properties here
                    AppId = app.AppId,
                    AccId = app.AccId,
                    Name = app.Name,
                    Publisher = app.Publisher,
                    Version = app.Version,
                    Release = app.Release,
                    Type = app.Type,
                    Os = app.Os,
                    Osversion = app.Osversion,
                    Description = app.Description,
                    Download = app.Download,
                    Docs = app.Docs,
                    Language = app.Language,
                    Db = app.Db,
                  
                })
                .ToListAsync();

            if (applications == null || !applications.Any())
            {
                return NotFound(); // Or you can return any other status code as needed
            }

            return applications;
        }
        //        //public async Task<List<Software>> GetSoftwareForDeviceAsync(int deviceId)
        //        //{
        //        //    // Thực hiện truy vấn để lấy danh sách phần mềm cho tài khoản cụ thể
        //        //    var softwaresForDevice = await _context.Softwares
        //        //       .Where(software => software.DeviceId == deviceId) // Lọc theo ID tài khoản
        //        //       .ToListAsync();

        //        //    return softwaresForDevice;
        //        //}


        //[HttpGet("list_App/{key}")]
        //public async Task<IActionResult> GetAppAsync(int key)
        //{
        //    var ressult = await _AppService.GetAppAsync(key);
        //    return StatusCode(StatusCodes.Status200OK, ressult);
        //}

        //[HttpGet("GetAppForAccountAndDevice")]
        //public async Task<ActionResult<IEnumerable<AppDto>>> GetAppForAccountAndDevice(int accountId, int deviceId)
        //{
        //    var AppForAccountAndDevice = await _context.Apps
        //        .Where(App => App.AccId == accountId)
        //        .Where(App => App.DeviceApps.Any(ds => ds.DeviceId == deviceId))
        //        .Select(App => new AppDto
        //        {
        //            AppId = App.AppId,
        //            AccId = App.AccId,
        //            Name = App.Name,
        //            Publisher = App.Publisher,
        //            Version = App.Version,
        //            Release = App.Release,
        //            Type = App.Type,
        //            Os = App.Os,
        //            Description = App.Description,
        //            Docs = App.Docs,
        //            Download = App.Download,
        //            Status = App.DeviceApps.FirstOrDefault(ds => ds.DeviceId == deviceId).Status,
        //            // Lấy InstallDate từ bảng liên quan
        //            InstallDate = App.DeviceApps.FirstOrDefault(ds => ds.DeviceId == deviceId).InstallDate.ToString("dd/MM/yyyy")
        //        })
        //        .ToListAsync();

        //    return AppForAccountAndDevice;
        //}
        //[HttpGet("GetAppByReport/Type")]
        //public async Task<ActionResult<IEnumerable<AppDto>>> GetAppByReport(string reportType)
        //{
        //    try
        //    {
        //        // Truy vấn danh sách App dựa trên ReportId
        //        var AppList = await _context.Apps
        //            .Where(s => s.Reports.Any(r => r.Type.ToLower() == reportType.ToLower()))
        //            .Select(s => new AppDto
        //            {
        //                AppId = s.AppId,
        //                AccId = s.AccId,
        //                Name = s.Name,
        //                Publisher = s.Publisher,
        //                Version = s.Version,
        //                Release = s.Release,
        //                Type = s.Type,
        //                Os = s.Os,
        //                Description = s.Description,
        //                Download = s.Download,
        //                Docs = s.Docs,
        //                Status = s.Status
        //            })
        //            .ToListAsync();

        //        return Ok(AppList);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Đã xảy ra lỗi: " + ex.Message);
        //    }
        //}
        //[HttpGet("GetApp/{deviceId}")]
        //public async Task<ActionResult<IEnumerable<AppDto>>> GetAppForDevice(int deviceId)
        //{
        //    var AppForAccountAndDevice = await _context.Apps
        //        .Where(App => App.DeviceApps.Any(ds => ds.DeviceId == deviceId))
        //        .Select(App => new AppDto
        //        {
        //            AppId = App.AppId,
        //            AccId = App.AccId,
        //            Name = App.Name,
        //            Publisher = App.Publisher,
        //            Version = App.Version,
        //            Release = App.Release,
        //            Type = App.Type,
        //            Os = App.Os,
        //            Description = App.Description,
        //            Docs = App.Docs,
        //            Download = App.Download,
        //            Status = App.DeviceApps.FirstOrDefault(ds => ds.DeviceId == deviceId).Status,
        //            // Lấy InstallDate từ bảng liên quan
        //            InstallDate = App.DeviceApps.FirstOrDefault(ds => ds.DeviceId == deviceId).InstallDate.ToString("dd/MM/yyyy")
        //        })
        //        .ToListAsync();

        //    return AppForAccountAndDevice;
        //}

    }
}
