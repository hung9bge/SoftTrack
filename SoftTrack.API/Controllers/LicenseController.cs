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
        public LicenseController(IConfiguration configuration, soft_track5Context context)
        {
            _configuration = configuration;
            _context = context;
        }
        //[HttpGet("ListLicenses")]
        //public async Task<ActionResult<IEnumerable<LicenseDto>>> ListAllLicensesAsync()
        //{
        //    var lst = await _context.Licenses
        //        .Select(item => new LicenseDto
        //        {
        //            LicenseId = item.LicenseId,
        //            LicenseKey = item.LicenseKey,
        //            Start_Date = item.Start_Date,
        //            Time = item.Time,
        //            Status = item.Status
        //        })
        //        .ToListAsync();

        //    return lst;
        //}

        [HttpGet("list_Licenses_by_Asset/{key}")]
        public async Task<ActionResult<IEnumerable<LisenceListDto>>> GetLicensesByAssetAsync(int key)
        {
            var lst = await _context.AssetSoftwares
                .Where(item => item.AssetId == key)
                .Select(item => new LisenceListDto
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
        [HttpPut("UpdateLicense/{key}")]
        public async Task<IActionResult> UpdateLicenseAsync(int key, [FromBody] LicenseUpdateDto updatedLicenseDto)
        {
            if (updatedLicenseDto == null)
            {
                return BadRequest(ModelState);
            }
            var updatedLicense = await _context.Licenses.FindAsync(key);

            if (updatedLicense == null)
            {
                return NotFound("License not found");
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
            

            if (updatedLicenseDto.Status == 0)
            {
                updatedLicense.Status = 0;
            }

            await _context.SaveChangesAsync();

            return Ok("License updated successfully");
        }
        [HttpDelete("DeleteLicenseWith_key")]
        public async Task<IActionResult> DeleteLicenseAsync(int id)
        {
            var item = await _context.Licenses.FindAsync(id);

            if (item != null)
            {
                item.Status = 3 ;
                await _context.SaveChangesAsync();
            }
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
