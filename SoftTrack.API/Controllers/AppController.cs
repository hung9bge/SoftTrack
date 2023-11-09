using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Application.DTO;
using SoftTrack.Domain;

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

        [HttpGet("ListApp")]
        public async Task<ActionResult<IEnumerable<Domain.Application>>> GetAllAppAsync()
        {
            var listApplications = await _context.Applications.ToListAsync();
            return listApplications;
        }

        //[HttpPost("CreateApp")]
        //public async Task<IActionResult> CreateAppAsync(AppCreateDto AppCreateDto)
        //{
        //    await _AppService.CreateAppAsync(AppCreateDto);
        //    return Ok("App đã được Add thành công.");

        //}

        //[HttpPut("UpdateSW{key}")]
        //public async Task<IActionResult> UpdateAppAsync(int key, AppUpdateDto updatedApp)
        //{
        //    await _AppService.UpdateAppAsync(key, updatedApp);
        //    return Ok("App đã được cập nhật thành công.");

        //}

        //[HttpDelete("DeleteAppWith_key")]
        //public async Task<IActionResult> DeleteAppAsync(int Appid)
        //{
        //    await _AppService.DeleteAppAsync(Appid);
        //    return StatusCode(StatusCodes.Status200OK);
        //}
        //[HttpGet("list_App_by_user/{key}")]
        //public async Task<IActionResult> GetAppForAccountAsync(int key)
        //{
        //    var ressult = await _AppService.GetAppForAccountAsync(key);
        //    return StatusCode(StatusCodes.Status200OK, ressult);
        //}
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
