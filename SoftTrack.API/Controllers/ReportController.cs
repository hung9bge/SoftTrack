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

        [HttpPost("CreateReport")]
        public async Task<IActionResult> CreateReport([FromBody] ReportCreateDto reportCreateDto)
        {
            if (reportCreateDto == null)
            {
                return BadRequest("Dữ liệu không hợp lệ.");
            }

            var newReport = new Report
            {
                ReportId= reportCreateDto.ReportId,
                SoftwareId = reportCreateDto.SoftwareId,
                Description = reportCreateDto.Description,
                Type = reportCreateDto.Type,
                StartDate = DateTime.Parse(reportCreateDto.StartDate),
                EndDate = DateTime.Parse(reportCreateDto.EndDate),
                Status = reportCreateDto.Status
            };

            _context.Reports.Add(newReport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReport", new { id = newReport.ReportId }, newReport);

        }

        // PUT: api/Reports/5
        [HttpPut("UpdateReport/{id}")]
        public async Task<IActionResult> UpdateReport(int id, [FromBody] ReportUpdateDto reportUpdateDto)
        {

                var existingReport = await _context.Reports.FindAsync(id);

                if (existingReport == null)
                {
                    return NotFound();
                }

                if (reportUpdateDto.SoftwareId != 0)
                {
                    existingReport.SoftwareId = reportUpdateDto.SoftwareId;
                }

                if (reportUpdateDto.Description != "string")
                {
                    existingReport.Description = reportUpdateDto.Description;
                }

                if (reportUpdateDto.Type != "string" )
                {
                    existingReport.Type = reportUpdateDto.Type;
                }

                if (reportUpdateDto.StartDate != "string")
                {
                    existingReport.StartDate = DateTime.Parse(reportUpdateDto.StartDate);
                }

                if (reportUpdateDto.EndDate != "string")
                {
                    existingReport.EndDate = DateTime.Parse(reportUpdateDto.EndDate);
                }

                if (reportUpdateDto.Status != 0)
                {
                    existingReport.Status = reportUpdateDto.Status;
                }

                _context.Reports.Update(existingReport);
                await _context.SaveChangesAsync();

                return Ok("Report đã được cập nhật thành công.");
            
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

            return Ok("Report đã được xóa thành công.");
        }
        [HttpGet("list_reports_by_account/{accountId}")]
        public async Task<IActionResult> GetReportsForAccountAsync(int accountId)
        {
            var reports = _context.Reports.Where(r => r.Software.AccId == accountId).Select(report => new ReportDto
            {
                ReportId = report.ReportId,
                SoftwareId = report.SoftwareId,
                Description = report.Description,
                Type = report.Type,
                StartDate = report.StartDate.ToString("yyyy-MM-dd"),
                EndDate = report.EndDate.ToString("yyyy-MM-dd"),
                Status = report.Status
            }).ToList();

            return Ok(reports);
        }

    }

}
