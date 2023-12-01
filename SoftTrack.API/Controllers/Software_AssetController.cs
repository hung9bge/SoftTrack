﻿using Microsoft.AspNetCore.Mvc;
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
        //[HttpPost("CreateAssetSoftwareNoLicense")]
        //public async Task<IActionResult> CreateAssetSoftwareAsync([FromBody] AssetSoftwareDto assetSoftwareDto)
        //{
        //    if (assetSoftwareDto.AssetId != 0 && assetSoftwareDto.SoftwareId != 0)
        //    {
        //        var assetSoftware = new AssetSoftware
        //        {
        //            AssetId = assetSoftwareDto.AssetId,
        //            SoftwareId = assetSoftwareDto.SoftwareId,
        //            Status = assetSoftwareDto.Status
        //        };
        //        if (DateTime.TryParseExact(assetSoftwareDto.InstallDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
        //        {
        //            assetSoftware.InstallDate = parsedDate;
        //        }
        //        _context.AssetSoftwares.Add(assetSoftware);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetAssetSoftware", new { AssetId = assetSoftware.AssetId, SoftwareId = assetSoftware.SoftwareId }, assetSoftware);
        //    }
        //    else
        //    {
        //        return Ok("Vui lòng điền thông tin.");
        //    }
        //}

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
        //[HttpDelete("DeleteAssetSoftware/{assetId}/{softwareId}")]
        //public async Task<IActionResult> DeleteAssetSoftwareAsync(int assetId, int softwareId)
        //{
        //    var assetSoftware = await _context.AssetSoftwares.FindAsync(assetId, softwareId);

        //    if (assetSoftware == null)
        //    {
        //        return NotFound();
        //    }
        //    assetSoftware.Status = 3;
        //    Xóa dữ liệu từ bảng License có LicenseId tương ứng
        //    var license = await _context.Licenses.FindAsync(assetSoftware.LicenseId);
        //    if (license != null)
        //    {
        //        license.Status = 3;
        //    }

        //    await _context.SaveChangesAsync();

        //    return Ok("AssetSoftware and related License deleted successfully");
        //}
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
                if (newLicense != null)
                {
                    _context.Licenses.Add(newLicense);
                    await _context.SaveChangesAsync();
                }
                //Thêm giấy phép vào DbSet Licenses
                
                

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
