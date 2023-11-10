using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Manage.DTO;
using SoftTrack.Domain;
using System.Globalization;

namespace SoftTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Asset_AppController : ControllerBase
    {
        private readonly soft_track4Context _context;

        public Asset_AppController(soft_track4Context context)
        {
            _context = context;
        }

        [HttpPut("UpdateAssetApplication/{assetId}/{appId}")]
        public async Task<IActionResult> UpdateAssetApplicationAsync(int assetId, int appId, [FromBody] AssetApplicationDTO updatedAssetAppDto)
        {
            var updatedAssetApp = await _context.AssetApplications
                .FirstOrDefaultAsync(aa => aa.AssetId == assetId && aa.AppId == appId);

            if (updatedAssetApp == null)
            {
                return NotFound("AssetApplication not found");
            }

            // Cập nhật các trường cần thiết của AssetApplication
            if (updatedAssetAppDto.InstallDate != "string")
            {
                updatedAssetApp.InstallDate = DateTime.Parse(updatedAssetAppDto.InstallDate);
            }
            if (updatedAssetAppDto.Status == 0)
            {
                updatedAssetApp.Status = updatedAssetAppDto.Status;
            }
            // Cập nhật các trường khác nếu cần

            await _context.SaveChangesAsync();

            return Ok("AssetApplication updated successfully");
        }
        [HttpDelete("DeleteAssetApplication/{assetId}/{appId}")]
        public async Task<IActionResult> DeleteAssetApplicationAsync(int assetId, int appId)
        {
            var assetAppToDelete = await _context.AssetApplications
                .FirstOrDefaultAsync(aa => aa.AssetId == assetId && aa.AppId == appId);

            if (assetAppToDelete != null)
            {
                assetAppToDelete.Status = 3;
                await _context.SaveChangesAsync();

                return Ok("assets đã được xóa thành công.");
            }     
            return NotFound("AssetApplication not found");
        }

        [HttpPost("CreateAssetApplication")]
        public async Task<IActionResult> CreateAssetApplicationAsync([FromBody] AssetApplicationDTO assetAppDto)
        {
            if (ModelState.IsValid)
            {
                var assetApp = new AssetApplication
                {
                    AssetId = assetAppDto.AssetId,
                    AppId = assetAppDto.AppId,
                    Status = assetAppDto.Status
                };
                if (DateTime.TryParseExact(assetAppDto.InstallDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                {
                    assetApp.InstallDate = parsedDate;
                }
                _context.AssetApplications.Add(assetApp);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetAssetApplication", new { assetId = assetApp.AssetId, appId = assetApp.AppId }, assetApp);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }


    }
}


