using Microsoft.AspNetCore.Mvc;
using SoftTrack.Application.DTO;
using SoftTrack.Domain;

namespace SoftTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Device_SoftwareController : ControllerBase
    {
        private readonly soft_track3Context _context;

        public Device_SoftwareController(soft_track3Context context)
        {
            _context = context;
        }

        [HttpPost("CreateLisence")]
        public async Task<IActionResult> CreateLisence([FromBody] LisenceCreateDto lisenceCreateDto)
        {
            try
            {
                // Kiểm tra xem Device và Software tồn tại
                var device = await _context.Devices.FindAsync(lisenceCreateDto.DeviceId);
                var software = await _context.Softwares.FindAsync(lisenceCreateDto.SoftwareId);

                if (device == null || software == null)
                {
                    return BadRequest("Device hoặc Software không tồn tại.");
                }

                // Tạo giấy phép
                var newLisence = new Lisence
                {
                    
                    LisenceKey = lisenceCreateDto.LisenceKey,                   
                    StartDate = DateTime.Parse(lisenceCreateDto.StartDate),
                    Time = lisenceCreateDto.Time,
                    Status = lisenceCreateDto.Status
                };

                // Thêm giấy phép vào DbSet Lisences
                _context.Lisences.Add(newLisence);

                // Tạo DeviceSoftware và thêm vào DbSet DeviceSoftwares
                var deviceSoftware = new DeviceSoftware
                {
                    DeviceId = device.DeviceId,
                    SoftwareId = software.SoftwareId,
                    LisenceId = newLisence.LisenceId, // ID của giấy phép mới tạo
                    InstallDate = DateTime.Now, // Hoặc giá trị khác nếu cần
                    Status = 1 // Hoặc giá trị khác nếu cần
                };
                _context.DeviceSoftwares.Add(deviceSoftware);

                await _context.SaveChangesAsync();

                return CreatedAtAction("GetLisence", new { id = newLisence.LisenceId }, newLisence);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

    }
}


