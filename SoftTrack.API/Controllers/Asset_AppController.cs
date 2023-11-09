//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SoftTrack.Application.DTO;
//using SoftTrack.Domain;
//using System.Globalization;

//namespace SoftTrack.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class Asset_AppController : ControllerBase
//    {
//        private readonly soft_track4Context _context;

//        public Asset_AppController(soft_track4Context context)
//        {
//            _context = context;
//        }

//        [HttpPost("CreateLicense")]
//        public async Task<IActionResult> CreateLicense([FromBody] LicenseCreateDto licenseCreateDto)
//        {
//            License newLicense = new License();
//            try
//            {
//                // Kiểm tra xem Device và Software tồn tại
//                var device = await _context.Devices.FindAsync(licenseCreateDto.DeviceId);
//                var software = await _context.Softwares.FindAsync(licenseCreateDto.SoftwareId);

//                if (device == null || software == null)
//                {
//                    return BadRequest("Device hoặc Software không tồn tại.");
//                }

//                // Tạo giấy phép

//                newLicense.LicenseKey = licenseCreateDto.LicenseKey;
//                newLicense.Time = licenseCreateDto.Time;
//                newLicense.Status = licenseCreateDto.Status;
//                if (DateTime.TryParseExact(licenseCreateDto.Start_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
//                {
//                    newLicense.Start_Date = parsedDate;
//                }

//                // Thêm giấy phép vào DbSet Licenses
//                _context.Licenses.Add(newLicense);
//                await _context.SaveChangesAsync();

//                // Tạo DeviceSoftware và thêm vào DbSet DeviceSoftwares
//                var deviceSoftware = new DeviceSoftware
//                {
//                    DeviceId = device.DeviceId,
//                    SoftwareId = software.SoftwareId,
//                    LicenseId = newLicense.LicenseId, // ID của giấy phép mới tạo
//                    InstallDate = DateTime.Now, // Hoặc giá trị khác nếu cần
//                    Status = 1 // Hoặc giá trị khác nếu cần
//                };
//                _context.DeviceSoftwares.Add(deviceSoftware);

//                await _context.SaveChangesAsync();
//                return Ok("Licenses đã được thêm thành công.");

//                //return CreatedAtAction("GetLicense", new { id = newLicense.LicenseId }, newLicense);
//            }
//            catch (Exception ex)
//            {
//                var removeLicense = await _context.Licenses.FirstOrDefaultAsync(l => l.LicenseId == newLicense.LicenseId);

//                if (removeLicense != null)
//                {
//                    // Nếu tìm thấy, xóa bản ghi License
//                    _context.Licenses.Remove(removeLicense);

//                    await _context.SaveChangesAsync();
//                }
               
//                    return StatusCode(500, "Đã xảy ra lỗi: " + ex.Message);
//             }           
//        }
//        [HttpGet("list-device-software")]
//        public async Task<IActionResult> GetDeviceSoftwareList()
//        {
//            try
//            {
//                var deviceSoftwareList = await _context.DeviceSoftwares
//                    .Select(ds => new DeviceSoftware
//                    {
//                        DeviceId = ds.DeviceId,
//                        SoftwareId = ds.SoftwareId,
//                        LicenseId = ds.LicenseId,
//                        InstallDate = ds.InstallDate,
//                        Status = ds.Status
//                    })
//                    .ToListAsync();

//                return Ok(deviceSoftwareList);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, "Đã xảy ra lỗi: " + ex.Message);
//            }
//        }
//        [HttpPut("update-device-software")]
//        public async Task<IActionResult> UpdateDeviceSoftware([FromBody] UpdateDeviceSoftwareDto updateDto)
//        {
//            try
//            {
//                // Kiểm tra xem có tồn tại dữ liệu DeviceSoftware với DeviceId, SoftwareId, và LicenseId được cung cấp
//                var deviceSoftware = await _context.DeviceSoftwares
//                    .Where(ds => ds.DeviceId == updateDto.DeviceId && ds.SoftwareId == updateDto.SoftwareId && ds.LicenseId == updateDto.LicenseId)
//                    .FirstOrDefaultAsync();

//                if (deviceSoftware == null)
//                {
//                    return NotFound("DeviceSoftware không tồn tại.");
//                }

//                // Cập nhật thông tin DeviceSoftware từ DTO
//                deviceSoftware.InstallDate = updateDto.InstallDate;
//                deviceSoftware.Status = updateDto.Status;

//                // Lưu thay đổi vào cơ sở dữ liệu
//                await _context.SaveChangesAsync();

//                return Ok("Cập nhật DeviceSoftware thành công.");
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, "Đã xảy ra lỗi: " + ex.Message);
//            }
//        }

//    }
//}


