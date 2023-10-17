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

        [HttpGet]
        public async Task<IActionResult> GetAllSoftwareAsync()
        {
            var ressult = await _softwareService.GetAllSoftwareAsync();
            return StatusCode(StatusCodes.Status200OK, ressult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSoftwareAsync(SoftwareCreateDto softwareCreateDto)
        {
            await _softwareService.CreateSoftwareAsync(softwareCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSoftwareAsync(SoftwareUpdateDto softwareUpdateDto)
        {
            await _softwareService.UpdateSoftwareAsync(softwareUpdateDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSoftwareAsync(SoftwareDto softwareDto)
        {
            await _softwareService.DeleteSoftwareAsync(softwareDto);
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpGet("list_software_by_user{key}")]
        public async Task<IActionResult> GetSoftwareForAccountAsync(int key)
        {
            var ressult = await _softwareService.GetSoftwareForAccountAsync(key);
            return StatusCode(StatusCodes.Status200OK, ressult);
        }
    }
}
