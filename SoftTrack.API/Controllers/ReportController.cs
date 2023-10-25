using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Application.DTO.Report;
using SoftTrack.Application.Interface;
using SoftTrack.Domain;

namespace SoftTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly soft_track2Context _context;

        public ReportController(soft_track2Context context)
        {
            _context = context;
        }

        // GET: api/Reports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetReports()
        {
            var reports = await _context.Reports.ToListAsync();

            var reportDtos = reports.Select(report => new ReportDto
            {
                ReportId = report.ReportId,
                SoftwareId = report.SoftwareId,
                Description = report.Description,
                Type = report.Type,
                StartDate = report.StartDate.ToString("yyyy-MM-dd"),
                EndDate = report.EndDate.ToString("yyyy-MM-dd"), 
                Status = report.Status
            });

            return reportDtos.ToList();
        }

        // GET: api/Reports/5
        [HttpGet("ReportWith{id}")]
        public async Task<ActionResult<ReportDto>> GetReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            // Ánh xạ dữ liệu từ đối tượng Report sang đối tượng ReportDto
            var reportDto = new ReportDto
            {
                ReportId = report.ReportId,
                SoftwareId = report.SoftwareId,
                Description = report.Description,
                Type = report.Type,
                StartDate = report.StartDate.ToString("yyyy-MM-dd"),
                EndDate = report.EndDate.ToString("yyyy-MM-dd"),
                Status = report.Status
            };

            return reportDto;
        }
        [HttpGet("GetReportsForSoftware/{softwareId}")]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetReportsForSoftware(int softwareId)
        {
            var reportsForSoftware = await _context.Reports
                .Where(report => report.SoftwareId == softwareId)
                .Select(report => new ReportDto
                {
                    ReportId = report.ReportId,
                    SoftwareId = report.SoftwareId,
                    Description = report.Description,
                    Type = report.Type,
                    StartDate = report.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = report.EndDate.ToString("yyyy-MM-dd"),
                    Status = report.Status
                })
                .ToListAsync();

            return reportsForSoftware;
        }

        // POST: api/Reports
        [HttpPost]
        public async Task<ActionResult<Report>> PostReport(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReport", new { id = report.ReportId }, report);
        }

        // PUT: api/Reports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport(int id, Report report)
        {
            if (id != report.ReportId)
            {
                return BadRequest();
            }

            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Reports/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Report>> DeleteReport(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();

            return report;
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.ReportId == id);
        }
    }

}
