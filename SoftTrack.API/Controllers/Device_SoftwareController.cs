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

        [HttpPost("CreateLicense")]
        public async Task<IActionResult> CreateLicense([FromBody] LicenseCreateDto licenseCreateDto)
        {
            try
            {
                // Kiểm tra xem Device và Software tồn tại
                var device = await _context.Devices.FindAsync(licenseCreateDto.DeviceId);
                var software = await _context.Softwares.FindAsync(licenseCreateDto.SoftwareId);

                if (device == null || software == null)
                {
                    return BadRequest("Device hoặc Software không tồn tại.");
                }

                // Tạo giấy phép
                var newLicense = new License
                {
                    
                    LicenseKey = licenseCreateDto.LicenseKey,                   
                    StartDate = DateTime.Parse(licenseCreateDto.StartDate),
                    Time = licenseCreateDto.Time,
                    Status = licenseCreateDto.Status
                };

                // Thêm giấy phép vào DbSet Licenses
                _context.Licenses.Add(newLicense);

                // Tạo DeviceSoftware và thêm vào DbSet DeviceSoftwares
                var deviceSoftware = new DeviceSoftware
                {
                    DeviceId = device.DeviceId,
                    SoftwareId = software.SoftwareId,
                    LicenseId = newLicense.LicenseId, // ID của giấy phép mới tạo
                    InstallDate = DateTime.Now, // Hoặc giá trị khác nếu cần
                    Status = 1 // Hoặc giá trị khác nếu cần
                };
                _context.DeviceSoftwares.Add(deviceSoftware);

                await _context.SaveChangesAsync();

                return CreatedAtAction("GetLicense", new { id = newLicense.LicenseId }, newLicense);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

    }
}


