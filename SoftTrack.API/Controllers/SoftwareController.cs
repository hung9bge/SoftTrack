using Microsoft.AspNetCore.Mvc;
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
        public SoftwareController(ISoftwareService softwareService)
        {
            _softwareService = softwareService;
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
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("UpdateSW")]
        public async Task<IActionResult> UpdateSoftwareAsync(SoftwareUpdateDto softwareUpdateDto)
        {
            await _softwareService.UpdateSoftwareAsync(softwareUpdateDto);
            return Ok("SoftWare đã được cập nhật thành công.");
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSoftwareAsync(int softwareid)
        {
            await _softwareService.DeleteSoftwareAsync(softwareid);
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpGet("list_software_by_user{key}")]
        public async Task<IActionResult> GetSoftwareForAccountAsync(int key)
        {
            var ressult = await _softwareService.GetSoftwareForAccountAsync(key);
            return StatusCode(StatusCodes.Status200OK, ressult);
        }
        //[HttpGet("list_software_by_device{key}")]
        //public async Task<IActionResult> GetSoftwareForDeviceAsync(int key)
        //{
        //    var ressult = await _softwareService.GetSoftwareForDeviceAsync(key);
        //    return StatusCode(StatusCodes.Status200OK, ressult);
        //}
    }
}
