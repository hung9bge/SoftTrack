using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Manage.DTO;
using SoftTrack.Domain;
using System.Globalization;
using SoftTrack.Manage.DTO.Asset_App;

namespace SoftTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Asset_AppController : ControllerBase
    {
        private readonly soft_track5Context _context;

        public Asset_AppController(soft_track5Context context)
        {
            _context = context;
        }

        [HttpPut("UpdateAssetApplication/{assetId}/{appId}")]
        public async Task<IActionResult> UpdateAssetApplicationAsync(int assetId, int appId, [FromBody] Asset_AppUpdateDto updatedAssetAppDto)
        {
            var updatedAssetApp = await _context.AssetApplications
                .FirstOrDefaultAsync(aa => aa.AssetId == assetId && aa.AppId == appId);

            if (updatedAssetApp == null)
            {
                return NotFound();
            }

            // Cập nhật các trường cần thiết của AssetApplication         
            if (DateTime.TryParseExact(updatedAssetAppDto.InstallDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate) && updatedAssetAppDto.InstallDate != "string")
            {
                updatedAssetApp.InstallDate = parsedDate;
            }          
           
            if (updatedAssetAppDto.Status != 0)
            {
                updatedAssetApp.Status = updatedAssetAppDto.Status;
            }
            // Cập nhật các trường khác nếu cần
            _context.AssetApplications.Update(updatedAssetApp);

            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete("DeleteAssetApplication/{assetId}/{appId}")]
        public async Task<IActionResult> DeleteAssetApplicationAsync(int assetId, int appId)
        {
            var assetAppToDelete = await _context.AssetApplications
                .FirstOrDefaultAsync(aa => aa.AssetId == assetId && aa.AppId == appId);

            if (assetAppToDelete == null)
            {             
                return NotFound();              
            }
            _context.AssetApplications.Remove(assetAppToDelete);
            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpPost("CreateAssetApplication")]
        public async Task<IActionResult> CreateAssetApplicationAsync([FromBody] AssetApplicationDTO assetAppDto)
        {
            if (ModelState.IsValid)
            {
                if (assetAppDto.AssetId == 0 || assetAppDto.AppId == 0)
                {
                    return NotFound();

                }
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

                return CreatedAtAction("CreateAssetApplication", new { assetId = assetApp.AssetId, appId = assetApp.AppId }, assetApp);
            }
            else
            {
                return NotFound();
            }
        }


    }
}


