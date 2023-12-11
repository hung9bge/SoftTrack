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
                if(licenseDto.LicenseKey != "string" )
                {
                    newLicense.LicenseKey = licenseDto.LicenseKey;
                }
                if (licenseDto.Time != 0 )
                {
                    newLicense.Time = licenseDto.Time;
                }
                if (licenseDto.Status_License != 0)
                {
                    newLicense.Status = licenseDto.Status_License;
                }
                if (licenseDto.Start_Date != "string")
                {
                    if (DateTime.TryParseExact(licenseDto.Start_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                    {
                        newLicense.StartDate = parsedDate;
                    }
                }
               
                if (newLicense.LicenseKey != null && newLicense.Status != 0 && newLicense.StartDate != null)
                {
                    _context.Licenses.Add(newLicense);
                    await _context.SaveChangesAsync();
                }
                //Tạo AssetSoftware và thêm vào DbSet AssetSoftware
                var assetSoftware = new AssetSoftware
                {
                    AssetId = licenseDto.AssetId,
                    SoftwareId = licenseDto.SoftwareId,                                
                    InstallDate = DateTime.Now, // Hoặc giá trị khác nếu cần
                    Status = 1 // Hoặc giá trị khác nếu cần
                };
                if (newLicense.LicenseId != 0)
                {
                    assetSoftware.LicenseId = newLicense.LicenseId; // ID của giấy phép mới tạo
                }
                _context.AssetSoftwares.Add(assetSoftware);

                await _context.SaveChangesAsync();
                return Ok("Software Asset đã được tạo thành công.");
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
        [HttpPost("CreateWithHaveLicenseAndSoftware")]
        public async Task<IActionResult> CreateLicenseSoftware([FromBody] AssetSoftwareDto licenseDto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Kiểm tra xem Device tồn tại
                    var asset = await _context.Assets.FindAsync(licenseDto.AssetId);

                    if (asset == null)
                    {
                        return BadRequest("Asset không tồn tại.");
                    }

                    // Tạo mới Software
                    var newSoftware = new Software
                    {
                        Name = licenseDto.Name,
                        Publisher = licenseDto.Publisher,
                        Version = licenseDto.Version,
                        Release = licenseDto.Release,
                        Type = licenseDto.Type,
                        Os = licenseDto.Os,
                        Status = licenseDto.Status_Software
                    };
                    if (newSoftware == null)
                    {
                        return BadRequest("Chưa nhập Software.");
                        
                    }
                    _context.Softwares.Add(newSoftware);
                    await _context.SaveChangesAsync();

                    // Tạo mới License
                    var newLicense = new License
                    {
                        LicenseKey = licenseDto.LicenseKey,
                        Time = licenseDto.Time,
                        Status = licenseDto.Status_License,
                        StartDate = DateTime.ParseExact(licenseDto.Start_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    };
                    if (newLicense != null)
                    {
                        _context.Licenses.Add(newLicense);
                        await _context.SaveChangesAsync();
                    }               

                    // Tạo mới AssetSoftware và thêm vào DbSet AssetSoftware
                    var assetSoftware = new AssetSoftware
                    {
                        AssetId = licenseDto.AssetId,
                        SoftwareId = newSoftware.SoftwareId,
                        LicenseId = newLicense.LicenseId,
                        InstallDate = DateTime.Now,
                        Status = licenseDto.Status_AssetSoftware
                    };

                    _context.AssetSoftwares.Add(assetSoftware);
                    await _context.SaveChangesAsync();

                    transaction.Commit();

                    return Ok("Licenses và Software đã được thêm thành công.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return StatusCode(500, "Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }

    }
}
