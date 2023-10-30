using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Application.DTO;
using SoftTrack.Application.DTO.Report;
using SoftTrack.Domain;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SoftTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LisenceController : ControllerBase
    {
        private readonly soft_track3Context _context;

        public LisenceController(soft_track3Context context)
        {
            _context = context;
        }

        // GET: api/Lisence
        [HttpGet]
        public async Task<IActionResult> GetLisences()
        {
            var lisences = await _context.Lisences.ToListAsync();
            return Ok(lisences);
        }
        [HttpGet("list_licenses_by_device/{deviceId}")]
        public async Task<IActionResult> GetLicensesByDevice(int deviceId)
        {
            // Sử dụng LINQ để lấy danh sách các bản quyền cho thiết bị có deviceId tương ứng
            var licenses = await _context.DeviceSoftwares
                .Where(ds => ds.DeviceId == deviceId)
                .Select(ds => ds.Lisence)
                .Select(license => new LisenceDto
                {
                    LisenceId = license.LisenceId,
                    LisenceKey = license.LisenceKey,                    
                    StartDate = license.StartDate.ToString("yyyy-MM-dd"),
                    Time = license.Time,
                    Status = license.Status
                })
                .ToListAsync();

            return Ok(licenses);
        }
        // GET: api/Lisence/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLisence(int id)
        {
            var lisence = await _context.Lisences.FindAsync(id);

            if (lisence == null)
            {
                return NotFound();
            }

            return Ok(lisence);
        }

        // POST: api/Lisence
        [HttpPost("CreateLisence")]
        public async Task<IActionResult> CreateLisence([FromBody] LisenceCreateDto lisenceDto)
        {
            var newLisence = new Lisence
            {
                LisenceKey= lisenceDto.LisenceKey,             
                StartDate = DateTime.Parse(lisenceDto.StartDate),
                Time = lisenceDto.Time,
                Status = lisenceDto.Status
            };

            _context.Lisences.Add(newLisence);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetLisence", new { id = newLisence.LisenceId }, newLisence);
        }

        // PUT: api/Lisence/5
        [HttpPut("UpdateLisence{id}")]
        public async Task<IActionResult> UpdateLisence(int id, [FromBody] LisenceUpdateDto lisence)
        {
            var existingLisence = await _context.Lisences.FindAsync(id);
            if (existingLisence == null)
            {
                return NotFound();
            }
            if (lisence.LisenceKey != "string")
            {
                existingLisence.LisenceKey = lisence.LisenceKey;
            }
            if (lisence.Time != 0)
            {
                existingLisence.Time = lisence.Time;
            }
            if (lisence.Status != 0)
            {
                existingLisence.Status = lisence.Status;
            }
     
            if (lisence.StartDate != "string")
            {
                existingLisence.StartDate = DateTime.Parse(lisence.StartDate);
            }
            _context.Lisences.Update(existingLisence);
            await _context.SaveChangesAsync();

            return Ok("Lisences đã được cập nhật thành công.");
        }

        // DELETE: api/Lisence/5
        [HttpDelete("DeleteLisence{id}")]
        public async Task<IActionResult> DeleteLisence(int id)
        {
            var lisence = await _context.Lisences.FindAsync(id);
            if (lisence == null)
            {
                return NotFound();
            }

            _context.Lisences.Remove(lisence);
            await _context.SaveChangesAsync();

            return Ok("Lisences đã được xóa thành công.");
        }
        
    }
}
