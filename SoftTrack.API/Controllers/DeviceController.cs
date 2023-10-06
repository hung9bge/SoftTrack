using Microsoft.AspNetCore.Mvc;
using SoftTrack.Application.DTO;
using SoftTrack.Application.Interface;
using SoftTrack.Domain;

namespace SoftTrack.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DeviceController : Controller
    {
        private readonly IDeviceService _DeviceService;
        public DeviceController(IDeviceService DeviceService)
        {
            _DeviceService = DeviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDeviceAsync()
        {
            var ressult = await _DeviceService.GetAllDeviceAsync();
            return StatusCode(StatusCodes.Status200OK, ressult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeviceAsync(DeviceCreateDto DeviceCreateDto)
        {
            await _DeviceService.CreateDeviceAsync(DeviceCreateDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDeviceAsync(DeviceUpdateDto DeviceUpdateDto)
        {
            await _DeviceService.UpdateDeviceAsync(DeviceUpdateDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDeviceAsync(DeviceDto DeviceDto)
        {
            await _DeviceService.DeleteDeviceAsync(DeviceDto);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
