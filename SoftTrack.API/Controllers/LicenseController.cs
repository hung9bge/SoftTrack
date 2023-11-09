//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SoftTrack.Application.DTO;
//using SoftTrack.Application.DTO.Report;
//using SoftTrack.Domain;
//using System;
//using System.Globalization;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SoftTrack.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LicenseController : ControllerBase
//    {
//        private readonly soft_track4Context _context;

//        public LicenseController(soft_track4Context context)
//        {
//            _context = context;
//        }

//        // GET: api/License
//        [HttpGet]
//        public async Task<IActionResult> GetLicenses()
//        {
//            var licenses = await _context.Licenses.ToListAsync();
//            return Ok(licenses);
//        }
//        [HttpGet("list_licenses_by_device/{deviceId}")]
//        public async Task<IActionResult> GetLicensesByDevice(int deviceId)
//        {
//            // Sử dụng LINQ để lấy danh sách các bản quyền cho thiết bị có deviceId tương ứng
//            var licenses = await _context.DeviceSoftwares
//               .Where(ds => ds.DeviceId == deviceId)
//               .Select(ds => new LicenseDto
//               {
//                   LicenseId = ds.License.LicenseId,
//                   LicenseKey = ds.License.LicenseKey,
//                   Start_Date = ds.License.Start_Date.ToString("dd/MM/yyyy"),
//                   Time = ds.License.Time,
//                   Status = ds.License.Status,
//                   SoftwareId = ds.SoftwareId // Bao gồm SoftwareId
//               })
//               .ToListAsync();

//            return Ok(licenses);
//        }
//        // GET: api/License/5
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetLicense(int id)
//        {
//            var license = await _context.Licenses.FindAsync(id);

//            if (license == null)
//            {
//                return NotFound();
//            }

//            return Ok(license);
//        }

//        // POST: api/License
//        //[HttpPost("CreateLicense")]
//        //public async Task<IActionResult> CreateLicense([FromBody] LicenseCreateDto licenseDto)
//        //{
//        //    var newLicense = new License
//        //    {
//        //        LicenseKey= licenseDto.LicenseKey,             
//        //        //Start_Date = DateTime.Parse(licenseDto.Start_Date),
//        //        Time = licenseDto.Time,
//        //        Status = licenseDto.Status
//        //    };
//        //    if (DateTime.TryParseExact(licenseDto.Start_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
//        //    {
//        //        newLicense.Start_Date = parsedDate;
//        //    }
//        //    _context.Licenses.Add(newLicense);
//        //    await _context.SaveChangesAsync();
//        //    return CreatedAtAction("GetLicense", new { id = newLicense.LicenseId }, newLicense);
//        //}

//        // PUT: api/License/5
//        [HttpPut("UpdateLicense{id}")]
//        public async Task<IActionResult> UpdateLicense(int id, [FromBody] LicenseUpdateDto license)
//        {
//            var existingLicense = await _context.Licenses.FindAsync(id);
//            if (existingLicense == null)
//            {
//                return NotFound();
//            }
//            if (license.LicenseKey != "string")
//            {
//                existingLicense.LicenseKey = license.LicenseKey;
//            }
//            if (license.Time != 0)
//            {
//                existingLicense.Time = license.Time;
//            }
//            if (license.Status != 0)
//            {
//                existingLicense.Status = license.Status;
//            }
     
//            if (license.Start_Date != "string")
//            {
//                existingLicense.Start_Date = DateTime.Parse(license.Start_Date);
//            }
//            _context.Licenses.Update(existingLicense);
//            await _context.SaveChangesAsync();

//            return Ok("Licenses đã được cập nhật thành công.");
//        }

//        // DELETE: api/License/5
//        [HttpDelete("DeleteLicense{id}")]
//        public async Task<IActionResult> DeleteLicense(int id)
//        {
//            // Kiểm tra xem có bản ghi DeviceSoftware liên quan đến giấy phép không
//            var deviceSoftware = await _context.DeviceSoftwares.FirstOrDefaultAsync(ds => ds.LicenseId == id);

//            if (deviceSoftware != null)
//            {
//                // Nếu tìm thấy, xóa bản ghi DeviceSoftware
//                _context.DeviceSoftwares.Remove(deviceSoftware);
//            }

//            var license = await _context.Licenses.FindAsync(id);

//            if (license == null)
//            {
//                return NotFound("Giấy phép không tồn tại.");
//            }

//            // Xóa giấy phép
//            _context.Licenses.Remove(license);
           
//            await _context.SaveChangesAsync();

//            return Ok("Giấy phép đã được xóa thành công.");
//        }
        
//    }
//}
