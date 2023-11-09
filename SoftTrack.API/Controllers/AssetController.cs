//using Microsoft.AspNetCore.Mvc;
//using SoftTrack.Application.DTO;
//using SoftTrack.Domain;
//using Microsoft.AspNetCore.OData;
//using Microsoft.AspNetCore.OData.Query;
//using Microsoft.AspNetCore.OData.Formatter;

//namespace SoftTrack.API.Controllers
//{
//    [Route("api/v1/[controller]")]
//    [ApiController]
//    public class AssetController : Controller
//    {
//        private readonly IDeviceService _DeviceService;
//        public AssetController(IDeviceService DeviceService)
//        {
//            _DeviceService = DeviceService;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAllDeviceAsync()
//        {
//            var ressult = await _DeviceService.GetAllDeviceAsync();
//            return StatusCode(StatusCodes.Status200OK, ressult);
//        }

//        [HttpPost]
//        public async Task<IActionResult> CreateDeviceAsync(DeviceCreateDto DeviceCreateDto)
//        {
//            await _DeviceService.CreateDeviceAsync(DeviceCreateDto);
//            return StatusCode(StatusCodes.Status201Created);
//        }


//        [HttpPut("UpdateDeviceWith{key}")]
//        public async Task<IActionResult> UpdateDeviceAsync(int key,DeviceUpdateDto DeviceUpdateDto)
//        {

//            await _DeviceService.UpdateDeviceAsync(key,DeviceUpdateDto);
//            return StatusCode(StatusCodes.Status201Created);
//        }

//        [HttpDelete("DeleteDeviceWith_key")]
//        public async Task<IActionResult> DeleteDeviceAsync(int DeviceId)
//        {
//            await _DeviceService.DeleteDeviceAsync(DeviceId);
//            return StatusCode(StatusCodes.Status200OK);
//        }


//        [HttpGet("list_device_with_sw{key}")]

//        public async Task<IActionResult> GetDevicesForSoftWareAsync(int key)
//        {
//            var ressult = await _DeviceService.GetDevicesForSoftWareAsync(key);
//            return StatusCode(StatusCodes.Status200OK, ressult);
//        }
//        [HttpGet("list_device_with_Account{key}")]

//        public async Task<IActionResult> GetDevicesForAccountAsync(int key)
//        {
//            var ressult = await _DeviceService.GetDevicesForAccountAsync(key);
//            return StatusCode(StatusCodes.Status200OK, ressult);
//        }
//    }
//}
