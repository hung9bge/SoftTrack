﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Application.DTO;
using SoftTrack.Application.Interface;
using SoftTrack.Application.Service;
using SoftTrack.Domain;

namespace SoftTrack.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SoftwareController : Controller
    {
        private readonly ISoftwareService _softwareService;
        private readonly soft_track2Context _context;
        public SoftwareController(ISoftwareService softwareService, soft_track2Context context)
        {
            _softwareService = softwareService;
            _context = context;
        }

        [HttpGet("ListSoftware")]
        public async Task<IActionResult> GetAllSoftwareAsync()
        {
            var ressult = await _softwareService.GetAllSoftwareAsync();
            return StatusCode(StatusCodes.Status200OK, ressult);
        }

        [HttpPost("CreateSW")]
        public async Task<IActionResult> CreateSoftwareAsync(SoftwareCreateDto softwareCreateDto)
        {
            await _softwareService.CreateSoftwareAsync(softwareCreateDto);
            return Ok("SoftWare đã được Add thành công.");
           
        }

        [HttpPut("UpdateSW{key}")]
        public async Task<IActionResult> UpdateSoftwareAsync(int key, SoftwareUpdateDto updatedSoftware)
        {
            await _softwareService.UpdateSoftwareAsync(key, updatedSoftware);
            return Ok("SoftWare đã được cập nhật thành công.");
           
        }

        [HttpDelete("DeleteSoftWareWith_key")]
        public async Task<IActionResult> DeleteSoftwareAsync(int softwareid)
        {
            await _softwareService.DeleteSoftwareAsync(softwareid);
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpGet("list_software_by_user/{key}")]
        public async Task<IActionResult> GetSoftwareForAccountAsync(int key)
        {
            var ressult = await _softwareService.GetSoftwareForAccountAsync(key);
            return StatusCode(StatusCodes.Status200OK, ressult);
        }
        [HttpGet("list_software/{key}")]
        public async Task<IActionResult> GetSoftwareAsync(int key)
        {
            var ressult = await _softwareService.GetSoftwareAsync(key);
            return StatusCode(StatusCodes.Status200OK, ressult);
        }

        [HttpGet("GetSoftwareForAccountAndDevice")]
        public async Task<ActionResult<IEnumerable<SoftwareDto>>> GetSoftwareForAccountAndDevice(int accountId, int deviceId)
        {
            var softwareForAccountAndDevice = await _context.Softwares
                .Where(software => software.AccId == accountId)
                .Where(software => software.DeviceSoftwares.Any(ds => ds.DeviceId == deviceId))
                .Select(software => new SoftwareDto
                {
                    SoftwareId = software.SoftwareId,
                    AccId= software.AccId,
                    Name = software.Name,
                    Publisher = software.Publisher,
                    Version = software.Version,
                    Release = software.Release,
                    Type = software.Type,
                    Os = software.Os,
                    Status = software.DeviceSoftwares.FirstOrDefault(ds => ds.DeviceId == deviceId).Status,
                    // Lấy InstallDate từ bảng liên quan
                    InstallDate = software.DeviceSoftwares.FirstOrDefault(ds => ds.DeviceId == deviceId).InstallDate.ToString("dd/MM/yyyy")
                })
                .ToListAsync();

            return softwareForAccountAndDevice;
        }
    }
}
