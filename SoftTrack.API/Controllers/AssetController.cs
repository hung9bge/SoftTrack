using Microsoft.AspNetCore.Mvc;
using SoftTrack.Software.DTO;
using SoftTrack.Domain;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.EntityFrameworkCore;

namespace SoftTrack.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AssetController : Controller
    {
        private readonly soft_track4Context _context;
        private readonly IConfiguration _configuration;
        public AssetController(IConfiguration configuration, soft_track4Context context)
        {
            _configuration = configuration;
            _context = context;
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
                    LastSuccesfullScan = aa.Asset.LastSuccesfullScan,
                    Status = aa.Asset.Status
                })
                .ToListAsync();

            if (!assetDtos.Any())
            {
                return NotFound();
            }

            return assetDtos;
        }
    }
}
