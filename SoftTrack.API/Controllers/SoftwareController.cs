using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using System.Globalization;

namespace SoftTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftwareController : Controller
    {
        private readonly soft_track5Context _context;
        private readonly IConfiguration _configuration;
        public SoftwareController(IConfiguration configuration, soft_track5Context context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpGet("ListSoftwares")]
        public async Task<ActionResult<IEnumerable<SoftwareDto>>> ListAllSoftwaresAsync()
        {
            var lst = await _context.Softwares
                .OrderBy(item => item.Status)        
                .Select(item => new SoftwareDto
                {
                    SoftwareId = item.SoftwareId,
                    Name = item.Name,
                    Publisher = item.Publisher,
                    Version = item.Version,
                    Release = item.Release,
                    Os = item.Os,
                    Type = item.Type,
                    Status = item.Status
                })
                .ToListAsync();

            return lst;
        }

        [HttpGet("list_Softwares_by_Asset/{key}")]
        public async Task<ActionResult<IEnumerable<SoftwareListDto>>> GetSoftwaresByAssetAsync(int key)
        {
            var lst = await _context.AssetSoftwares
                .Where(item => item.AssetId == key)
                .OrderBy(item => item.Status)
                .OrderBy(item => item.InstallDate)
                .Select(item => new SoftwareListDto
                {
                    SoftwareId = item.Software.SoftwareId,
                    Name = item.Software.Name,
                    Publisher = item.Software.Publisher,
                    Version = item.Software.Version,
                    Release = item.Software.Release,
                    Os = item.Software.Os,
                    Type = item.Software.Type,
                    InstallDate = item.InstallDate.HasValue ? item.InstallDate.Value.ToString("dd/MM/yyyy") : null,
                    AssetSoftwareStatus = item.Status
                })
                .ToListAsync();

            if (!lst.Any())
            {
                return NotFound();
            }

            return lst;
        }
        [HttpPost("CreateSoftware")]
        public async Task<IActionResult> CreateSoftwareAsync([FromBody] SoftwareCreateDto item)
        {
            if (ModelState.IsValid)
            {
                var tmp = new Software
                {
                    Name = item.Name,
                    Publisher = item.Publisher,
                    Version = item.Version,
                    Release = item.Release,
                    Type = item.Type,
                    Os = item.Os,
                    Status = item.Status
                };

                _context.Softwares.Add(tmp);
                await _context.SaveChangesAsync();

                return CreatedAtAction("CreateSoftware", new { id = tmp.SoftwareId }, tmp);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPut("UpdateSoftware/{key}")]
        public async Task<IActionResult> UpdateSoftwareAsync(int key, [FromBody] SoftwareUpdateDto updatedSoftwareDto)
        {
            if (updatedSoftwareDto == null)
            {
                return BadRequest(ModelState);
            }
            var updatedSoftware = await _context.Softwares.FindAsync(key);

            if (updatedSoftware == null)
            {
                return NotFound("Software not found");
            }

            if (updatedSoftwareDto.Name != null && updatedSoftwareDto.Name != "string")
            {
                updatedSoftware.Name = updatedSoftwareDto.Name;
            }

            if (updatedSoftwareDto.Publisher != null && updatedSoftwareDto.Publisher != "string")
            {
                updatedSoftware.Publisher = updatedSoftwareDto.Publisher;
            }

            if (updatedSoftwareDto.Version != null && updatedSoftwareDto.Version != "string")
            {
                updatedSoftware.Version = updatedSoftwareDto.Version;
            }

            if (updatedSoftwareDto.Release != null && updatedSoftwareDto.Release != "string")
            {
                updatedSoftware.Release = updatedSoftwareDto.Release;
            }

            if (updatedSoftwareDto.Type != null && updatedSoftwareDto.Type != "string")
            {
                updatedSoftware.Type = updatedSoftwareDto.Type;
            }

            if (updatedSoftwareDto.Os != null && updatedSoftwareDto.Os != "string")
            {
                updatedSoftware.Os = updatedSoftwareDto.Os;
            }

            if (updatedSoftwareDto.Status != 0)
            {
                updatedSoftware.Status = updatedSoftwareDto.Status;
            }
            _context.Softwares.Update(updatedSoftware);
            await _context.SaveChangesAsync();

            return Ok("Software updated successfully");
        }
        //[HttpDelete("DeleteSoftwareWith_key")]
        //public async Task<IActionResult> DeleteSoftwareAsync(int id)
        //{
        //    var item = await _context.Softwares.FindAsync(id);

        //    if (item != null)
        //    {
        //        item.Status = 3;

        //        await _context.SaveChangesAsync();
        //    }
        //    return Ok("Delete Software successfully!");
        //}
    }
}
