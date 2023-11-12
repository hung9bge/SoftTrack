using Microsoft.AspNetCore.Mvc;
using SoftTrack.Manage.DTO;
using SoftTrack.Domain;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace SoftTrack.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AssetController : Controller
    {
        private readonly soft_track5Context _context;
        private readonly IConfiguration _configuration;
        public AssetController(IConfiguration configuration, soft_track5Context context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpGet("ListAssets")]
        public async Task<ActionResult<IEnumerable<AssetDto>>> ListAllAssetsAsync()
        {
            var assetDtos = await _context.Assets
                .Select(asset => new AssetDto
                {
                    AssetId = asset.AssetId,
                    Name = asset.Name,
                    Cpu = asset.Cpu,
                    Gpu = asset.Gpu,
                    Ram = asset.Ram,
                    Memory = asset.Memory,
                    Os = asset.Os,
                    Version = asset.Version,
                    IpAddress = asset.IpAddress,
                    Bandwidth = asset.Bandwidth,
                    Manufacturer = asset.Manufacturer,
                    Model = asset.Model,
                    SerialNumber = asset.SerialNumber,
                    LastSuccesfullScan = asset.LastSuccesfullScan.HasValue ? asset.LastSuccesfullScan.Value.ToString("dd/MM/yyyy") : null,
                    Status = asset.Status
                })
                .ToListAsync();

            return assetDtos;
        }

        [HttpGet("list_Asset_by_App/{key}")]
        public async Task<ActionResult<IEnumerable<AssetDto>>> GetAssetsByAppAsync(int key)
        {
            var assetDtos = await _context.AssetApplications
                .Where(aa => aa.AppId == key) // Assuming 'key' is AppId
                .Select(aa => new AssetDto
                {
                    AssetId = aa.Asset.AssetId,
                    Name = aa.Asset.Name,
                    Cpu = aa.Asset.Cpu,
                    Gpu = aa.Asset.Gpu,
                    Ram = aa.Asset.Ram,
                    Memory = aa.Asset.Memory,
                    Os = aa.Asset.Os,
                    Version = aa.Asset.Version,
                    IpAddress = aa.Asset.IpAddress,
                    Bandwidth = aa.Asset.Bandwidth,
                    Manufacturer = aa.Asset.Manufacturer,
                    Model = aa.Asset.Model,
                    SerialNumber = aa.Asset.SerialNumber,
                    LastSuccesfullScan = aa.Asset.LastSuccesfullScan.HasValue ? aa.Asset.LastSuccesfullScan.Value.ToString("dd/MM/yyyy") : null,
                    Status = aa.Asset.Status
                })
                .ToListAsync();

            if (!assetDtos.Any())
            {
                return NotFound();
            }

            return assetDtos;
        }
        [HttpPost("CreateAsset")]
        public async Task<IActionResult> CreateAssetAsync([FromBody] AssetCreateDto assetDto)
        {
            if (ModelState.IsValid)
            {
                var asset = new Asset
                {
                    Name = assetDto.Name,
                    Cpu = assetDto.Cpu,
                    Gpu = assetDto.Gpu,
                    Ram = assetDto.Ram,
                    Memory = assetDto.Memory,
                    Os = assetDto.Os,
                    Version = assetDto.Version,
                    IpAddress = assetDto.IpAddress,
                    Bandwidth = assetDto.Bandwidth,
                    Manufacturer = assetDto.Manufacturer,
                    Model = assetDto.Model,
                    SerialNumber = assetDto.SerialNumber,
                    Status = assetDto.Status           
                };
                if (DateTime.TryParseExact(assetDto.LastSuccesfullScan, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                {
                    asset.LastSuccesfullScan = parsedDate;
                }
                _context.Assets.Add(asset);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAsset", new { id = asset.AssetId }, asset);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPut("UpdateAsset/{key}")]
        public async Task<IActionResult> UpdateAssetAsync(int key, [FromBody] AssetUpdateDto updatedAssetDto)
        {
            var updatedAsset = await _context.Assets.FindAsync(key);

            if (updatedAsset == null)
            {
                // Xử lý khi không tìm thấy tài sản
                // Ví dụ: Trả về lỗi hoặc thông báo
                return NotFound("Asset not found");
            }

            // Cập nhật các trường cần thiết của tài sản
            if (updatedAssetDto.Name != null && updatedAssetDto.Name != "string")
            {
                updatedAsset.Name = updatedAssetDto.Name;
            }

            if (updatedAssetDto.Cpu != null && updatedAssetDto.Cpu != "string")
            {
                updatedAsset.Cpu = updatedAssetDto.Cpu;
            }
            if (updatedAssetDto.Gpu != null && updatedAssetDto.Gpu != "string")
            {
                updatedAsset.Gpu = updatedAssetDto.Gpu;
            }
            if (updatedAssetDto.Ram != null && updatedAssetDto.Ram != "string")
            {
                updatedAsset.Ram = updatedAssetDto.Ram;
            }
            if (updatedAssetDto.Memory != null && updatedAssetDto.Memory != "string")
            {
                updatedAsset.Memory = updatedAssetDto.Memory;
            }
            if (updatedAssetDto.Os != null && updatedAssetDto.Os != "string")
            {
                updatedAsset.Os = updatedAssetDto.Os;
            }
            if (updatedAssetDto.Version != null && updatedAssetDto.Version != "string")
            {
                updatedAsset.Version = updatedAssetDto.Version;
            }
            if (updatedAssetDto.IpAddress != null && updatedAssetDto.IpAddress != "string")
            {
                updatedAsset.IpAddress = updatedAssetDto.IpAddress;
            }
            if (updatedAssetDto.Bandwidth != null && updatedAssetDto.Bandwidth != "string")
            {
                updatedAsset.Bandwidth = updatedAssetDto.Bandwidth;
            }
            if (updatedAssetDto.Manufacturer != null && updatedAssetDto.Manufacturer != "string")
            {
                updatedAsset.Manufacturer = updatedAssetDto.Manufacturer;
            }
            if (updatedAssetDto.Model != null && updatedAssetDto.Model != "string")
            {
                updatedAsset.Model = updatedAssetDto.Model;
            }
            if (updatedAssetDto.SerialNumber != null && updatedAssetDto.SerialNumber != "string")
            {
                updatedAsset.SerialNumber = updatedAssetDto.SerialNumber;
            }
            if (updatedAssetDto.Cpu != null && updatedAssetDto.Cpu != "string")
            {
                updatedAsset.Cpu = updatedAssetDto.Cpu;
            }
            if (updatedAssetDto.LastSuccesfullScan != "string")
            {
                updatedAsset.LastSuccesfullScan = DateTime.Parse(updatedAssetDto.LastSuccesfullScan);
            }
            if (updatedAssetDto.Status == 0 )
            {
                updatedAsset.Status = updatedAssetDto.Status;
            }


            // Gán các giá trị khác tương ứng từ updatedAssetDto

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            return Ok("Asset updated successfully");
        }
        [HttpDelete("DeleteAssetWith_key")]
        public async Task<IActionResult> DeleteAssetAsync(int assetId)
        {
            var assets = await _context.Assets.FindAsync(assetId);

            if (assets != null)
            {
                assets.Status = 3;

                await _context.SaveChangesAsync();
            }
            return Ok("assets đã được xóa thành công.");
        }


    }
}
