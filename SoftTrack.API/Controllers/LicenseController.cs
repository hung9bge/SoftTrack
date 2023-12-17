using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using System.Globalization;

namespace SoftTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseController : Controller
    {
        private readonly soft_track5Context _context;
        private readonly IConfiguration _configuration;
        public interface ILicenseController
        {
            Task<IEnumerable<LicenseDto>> ListAllLicensesAsync();
        }
        public LicenseController(IConfiguration configuration, soft_track5Context context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpGet("ListLicenses")]
        public async Task<ActionResult<IEnumerable<LisenceListDto>>> ListAllLicensesAsync()
        {
            var lst = await _context.Licenses
                  .Include(l => l.AssetSoftwares)
                  .OrderBy(l => l.Status)
                .OrderBy(l => l.StartDate)
                .Select(item => new LisenceListDto
                {
                    AssetId = item.AssetSoftwares.Select(la => la.AssetId).FirstOrDefault(),
                    SoftwareId = item.AssetSoftwares.Select(la => la.SoftwareId).FirstOrDefault(),
                    LicenseId = item.LicenseId,
                    LicenseKey = item.LicenseKey,
                    Start_Date = item.StartDate.HasValue ? item.StartDate.Value.ToString("dd/MM/yyyy") : null,
                    Time = item.Time,
                    Status = item.Status
                })
                .ToListAsync();
            if (!lst.Any())
            {
                return NotFound();
            }
            return lst;
        }

        [HttpGet("list_Licenses_by_Asset/{id}")]
        public async Task<ActionResult<IEnumerable<LisenceListByAssetDto>>> GetLicensesByAssetAsync(int id)
        {
            var lst = await _context.AssetSoftwares
                .Where(item => item.AssetId == id)
                 .OrderBy(l => l.Status)
                .OrderBy(l => l.InstallDate)
                .Select(item => new LisenceListByAssetDto
                {
                    SoftwareId = item.SoftwareId,
                    LicenseId = item.License.LicenseId,
                    LicenseKey = item.License.LicenseKey,
                    Start_Date = item.License.StartDate.HasValue ? item.License.StartDate.Value.ToString("dd/MM/yyyy") : null,
                    Time = item.License.Time,
                    Status = item.License.Status,
                    
                })
                .ToListAsync();

            if (!lst.Any())
            {
                return NotFound();
            }

            return lst;
        }
        //[HttpPost("CreateLicense")]
        //public async Task<IActionResult> CreateLicenseAsync([FromBody] LicenseCreateDto item)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var tmp = new License
        //        {
        //            LicenseKey = item.LicenseKey,
        //            Time = (item.Time > 0) ? item.Time : 0,
        //            Status = item.Status
        //        };
        //        if (DateTime.TryParseExact(item.Start_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
        //        {
        //            tmp.Start_Date = parsedDate;
        //        }

        //        _context.Licenses.Add(tmp);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetLicense", new { id = tmp.LicenseId }, tmp);
        //    }
        //    else
        //    {
        //        return BadRequest(ModelState);
        //    }
        //}
        [HttpPut("UpdateLicense/{id}")]
        public async Task<IActionResult> UpdateLicenseAsync(int id, [FromBody] LicenseUpdateDto updatedLicenseDto)
        {
            if (updatedLicenseDto == null)
            {
                return BadRequest(ModelState);
            }
            var updatedLicense = await _context.Licenses.FindAsync(id);

            if (updatedLicense == null)
            {
                return NotFound();
            }

            if (updatedLicenseDto.LicenseKey != null && updatedLicenseDto.LicenseKey != "string")
            {
                updatedLicense.LicenseKey = updatedLicenseDto.LicenseKey;
            }

            if (updatedLicenseDto.StartDate != null && updatedLicenseDto.StartDate != "string")
            {
                updatedLicense.StartDate = DateTime.Parse(updatedLicenseDto.StartDate);
            }       
                updatedLicense.Time = updatedLicenseDto.Time;      

            if (updatedLicenseDto.Status != 0)
            {
                updatedLicense.Status = updatedLicenseDto.Status;
            }
            _context.Licenses.Update(updatedLicense);
            await _context.SaveChangesAsync();

            return Ok();
        }
        //[HttpDelete("DeleteLicenseWith_key")]
        //public async Task<IActionResult> DeleteLicenseAsync(int id)
        //{
        //    var item = await _context.Licenses.FindAsync(id);

        //    if (item != null)
        //    {
        //        item.Status = 3 ;
        //        await _context.SaveChangesAsync();
        //    }
        //    return StatusCode(StatusCodes.Status200OK);
        //}
    }
}
