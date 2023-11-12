using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using System.Globalization;

namespace SoftTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Software_Asset : ControllerBase
    {
        private readonly soft_track5Context _context;

        public Software_Asset(soft_track5Context context)
        {
            _context = context;
        }
        [HttpPost("CreateAssetSoftwareNoLicense")]
        public async Task<IActionResult> CreateAssetSoftwareAsync([FromBody] AssetSoftwareDto assetSoftwareDto)
        {
            if (assetSoftwareDto.AssetId != 0 && assetSoftwareDto.SoftwareId != 0)
            {
                var assetSoftware = new AssetSoftware
                {
                    AssetId = assetSoftwareDto.AssetId,
                    SoftwareId = assetSoftwareDto.SoftwareId,
                    Status = assetSoftwareDto.Status
                };
                if (DateTime.TryParseExact(assetSoftwareDto.InstallDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                {
                    assetSoftware.InstallDate = parsedDate;
                }
                _context.AssetSoftwares.Add(assetSoftware);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAssetSoftware", new { AssetId = assetSoftware.AssetId, SoftwareId = assetSoftware.SoftwareId }, assetSoftware);
            }
            else
            {
                return Ok("Vui lòng điền thông tin.");
            }
        }

        [HttpDelete("DeleteAssetSoftware/{assetId}/{softwareId}")]
        public async Task<IActionResult> DeleteAssetSoftwareAsync(int assetId, int softwareId)
        {
            var assetSoftware = await _context.AssetSoftwares.FindAsync(assetId, softwareId);

            if (assetSoftware == null)
            {
                return NotFound();
            }

            // Xóa dữ liệu từ bảng AssetSoftware
            _context.AssetSoftwares.Remove(assetSoftware);

            // Xóa dữ liệu từ bảng License có LicenseId tương ứng
            var license = await _context.Licenses.FindAsync(assetSoftware.LicenseId);
            if (license != null)
            {
                _context.Licenses.Remove(license);
            }

            await _context.SaveChangesAsync();

            return Ok("AssetSoftware and related License deleted successfully");
        }
        [HttpPost("CreateWithHaveLicense")]
        public async Task<IActionResult> CreateLicense([FromBody] LicenseDto licenseDto)
        {
            License newLicense = new License();
            try
            {
                //Kiểm tra xem Device và Software tồn tại
                var asset = await _context.Assets.FindAsync(licenseDto.AssetId);
                var software = await _context.Softwares.FindAsync(licenseDto.SoftwareId);

                if (asset == null || software == null)
                {
                    return BadRequest("asset hoặc software không tồn tại.");
                }

                //Tạo giấy phép

                newLicense.LicenseKey = licenseDto.LicenseKey;
                newLicense.Time = licenseDto.Time;
                newLicense.Status = licenseDto.Status_License;
                if (DateTime.TryParseExact(licenseDto.Start_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                {
                    newLicense.StartDate = parsedDate;
                }

                //Thêm giấy phép vào DbSet Licenses
                _context.Licenses.Add(newLicense);
                await _context.SaveChangesAsync();

                //Tạo AssetSoftware và thêm vào DbSet AssetSoftware
                var assetSoftware = new AssetSoftware
                {
                    AssetId = licenseDto.AssetId,
                    SoftwareId = licenseDto.SoftwareId,
                    LicenseId = newLicense.LicenseId, // ID của giấy phép mới tạo
                    InstallDate = DateTime.Now, // Hoặc giá trị khác nếu cần
                    Status = 1 // Hoặc giá trị khác nếu cần
                };

                _context.AssetSoftwares.Add(assetSoftware);

                await _context.SaveChangesAsync();
                return Ok("Licenses đã được thêm thành công.");
            }
            catch (Exception ex)
            {
                var removeLicense = await _context.Licenses.FirstOrDefaultAsync(l => l.LicenseId == newLicense.LicenseId);

                if (removeLicense != null)
                {
                    //Nếu tìm thấy, xóa bản ghi License
                    _context.Licenses.Remove(removeLicense);

                    await _context.SaveChangesAsync();
                }

                return StatusCode(500, "Đã xảy ra lỗi: " + ex.Message);
            }
        }

    }
}
