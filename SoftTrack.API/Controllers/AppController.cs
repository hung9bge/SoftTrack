﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;

namespace SoftTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : Controller
    {
        private readonly soft_track5Context _context;
        private readonly IConfiguration _configuration;
        public interface IAppController
        {
            Task<IEnumerable<ApplicationDto>> ListAllAppsAsync();
        }
        public AppController(IConfiguration configuration, soft_track5Context context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet("ListApps")]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> ListAllAppsAsync()
        {
            var appDtos = await _context.Applications
                .OrderBy(app => app.Status)
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
                    Db = app.Db,
                    Status = app.Status,
                    
                })
                .ToListAsync();
            if (appDtos == null)
            {
                return NotFound();
            }
            return appDtos;
        }


        [HttpPost("CreateApp")]
        public async Task<IActionResult> CreateAppAsync([FromBody] ApplicationCreateDto appCreateDto)
        {
            if (ModelState.IsValid) // Kiểm tra tính hợp lệ của dữ liệu đầu vào
            {
                if(appCreateDto.AccId == 0)
                {
                    return NotFound();

                }
                // Tạo một đối tượng Application từ dữ liệu được chuyển đến từ đối tượng ApplicationCreateDto
                var application = new Application
                {   
                    AccId = appCreateDto.AccId,
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
                    Db = appCreateDto.Db,
                    Status= appCreateDto.Status,
                    // Gán các giá trị khác tương ứng từ appCreateDto
                };

                _context.Applications.Add(application);
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return NotFound();
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
            updatedApp.Status = (updatedAppDto.Status != null && updatedAppDto.Status != 0) ? updatedAppDto.Status : updatedApp.Status;

            // Lưu thay đổi vào cơ sở dữ liệu
            _context.Applications.Update(updatedApp);
            await _context.SaveChangesAsync();

            return Ok();
        }


        //[HttpDelete("DeleteAppWith_key")]
        //public async Task<IActionResult> DeleteAppAsync(int Appid)
        //{
        //    var deleteApp = await _context.Applications.FindAsync(Appid);

        //    if (deleteApp != null)
        //    {
        //        deleteApp.Status = 3;
        //        await _context.SaveChangesAsync();
        //        return Ok();
        //    }

        //    return NotFound();

        //}
        [HttpGet("list_App_by_user/{key}")]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetAppForAccountAsync(int key)
        {
            // Assuming 'key' is AccId
            var applications = await _context.Applications
                .Where(app => app.AccId == key)
                .OrderBy(app => app.Status)
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
                    Status = app.Status,
                  
                })
                .ToListAsync();

            if (applications == null || !applications.Any())
            {
                return NotFound(); // Or you can return any other status code as needed
            }

            return applications;
        }

        [HttpGet("get_App_by_Id/{key}")]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetAppByIdAsync(int key)
        {
            var lst = await _context.Applications
                .Where(app => app.AppId == key)
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
                    Db = app.Db,
                    Status = app.Status,
                })
                .ToListAsync();

            if (!lst.Any())
            {
                return NotFound();
            }

            return lst;
        }

    }
}
