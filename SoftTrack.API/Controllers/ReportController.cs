using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Domain;
using SoftTrack.Software.DTO;
using System.Globalization;
using System.Net;
using System.Net.Mail;


namespace SoftTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly soft_track4Context _context;
        
        public ReportController(soft_track4Context context)
        {
            _context = context;
        }

        // GET: api/Reports
        [HttpGet("ReportAll")]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetReports()
        {
            var reports = await _context.Reports.ToListAsync();

            var reportDtos = reports.Select(report => new ReportDto
            {
                ReportId = report.ReportId,
                AppId = report.AppId,
                Title = report.Title,
                Description = report.Description,
                Type = report.Type,
                Start_Date = report.Start_Date.ToString("dd/MM/yyyy"),
                End_Date = report.End_Date?.ToString("dd/MM/yyyy"), 
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
                AppId = report.AppId,
                Title = report.Title,
                Description = report.Description,
                Type = report.Type,
                Start_Date = report.Start_Date.ToString("dd/MM/yyyy"),
                End_Date = report.End_Date?.ToString("dd/MM/yyyy"),
                Status = report.Status
            };

            return reportDto;
        }
        [HttpGet("ReportsByType/{type}")]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetReportsByType(string type)
        {
            var reports = await _context.Reports
                .Where(report => report.Type == type)
                .Select(report => new ReportDto
                {
                    ReportId = report.ReportId,
                    AppId = report.AppId,
                    Title = report.Title,
                    Description = report.Description,
                    Type = report.Type,
                    Start_Date = report.Start_Date.ToString("dd/MM/yyyy"),
                    End_Date = report.End_Date.HasValue ? report.End_Date.Value.ToString("dd/MM/yyyy") : null,
                    Status = report.Status
                })
                .ToListAsync();

            if (!reports.Any())
            {
                return NotFound();
            }

            return reports;
        }

        [HttpGet("GetReportsForAppAndType/{AppId}/{type}")]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetReportsForSoftware(int AppId, string type)
        {
            var reportsForSoftware = await _context.Reports
                .Where(report => report.AppId == AppId && report.Type == type)
                .Select(report => new ReportDto
                {
                    ReportId = report.ReportId,
                    AppId = report.AppId,
                    Title = report.Title,   
                    Description = report.Description,
                    Type = report.Type,
                    Start_Date = report.Start_Date.ToString("dd/MM/yyyy"),
                    End_Date = report.End_Date.HasValue ? report.End_Date.Value.ToString("dd/MM/yyyy") : null,
                    Status = report.Status

                })
                .ToListAsync();

            return reportsForSoftware;
        }

        [HttpPost("CreateReport")]
        public async Task<IActionResult> CreateReport([FromBody] ReportCreateDto reportCreateDto)
        {
            string dateString = DateTime.Now.ToString("dd/MM/yyyy");
         
            var newReport = new Report
            {
             
                AppId = reportCreateDto.AppId,
                Title= reportCreateDto.Title,
                Description = reportCreateDto.Description,
                Type = reportCreateDto.Type,       
                Status = reportCreateDto.Status

            };

            if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                newReport.Start_Date = parsedDate;
            }
            if (DateTime.TryParseExact(reportCreateDto.End_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate1))
            {
                newReport.End_Date = parsedDate1;
            }

            _context.Reports.Add(newReport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReport", new { id = newReport.ReportId }, newReport);

        }

        // PUT: api/Reports/5
        [HttpPut("UpdateReport/{id}")]
        public async Task<IActionResult> UpdateReport(int id, [FromBody] ReportUpdateDto reportUpdateDto)
        {
            string dateString = DateTime.Now.ToString("dd/MM/yyyy");

            var existingReport = await _context.Reports.FindAsync(id);

                if (existingReport == null)
                {
                    return NotFound();
                }
                if (reportUpdateDto.Title != "string")
                {
                    existingReport.Title = reportUpdateDto.Title;
                }
                if (reportUpdateDto.Description != "string")
                {
                    existingReport.Description = reportUpdateDto.Description;
                }

                //if (reportUpdateDto.Type != "string" )
                //{
                //    existingReport.Type = reportUpdateDto.Type;
                //}
                //if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                //{
                //    existingReport.Start_Date = parsedDate;
                //}          
                if (!string.IsNullOrEmpty(reportUpdateDto.End_Date))
                {
                    existingReport.End_Date = DateTime.Parse(reportUpdateDto.End_Date);
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
            if (report != null)
            {
                report.Status = 3;
                await _context.SaveChangesAsync();
            }
            return Ok("Report đã được xóa thành công.");
        }

        [HttpGet("list_reports_by_account/{accountId}")]
        public async Task<IActionResult> GetReportsForAccountAsync(int accountId)
        {
            var reports = _context.Reports.Where(r => r.App.AccId == accountId).Select(report => new ReportDto
            {
                ReportId = report.ReportId,
                AppId = report.AppId,
                Title= report.Title,
                Description = report.Description,
                Type = report.Type,
                Start_Date = report.Start_Date.ToString("dd/MM/yyyy"),
                End_Date = report.End_Date.HasValue ? report.End_Date.Value.ToString("dd/MM/yyyy") : null,
                Status = report.Status
            }).ToList();

            return Ok(reports);
        }
        [HttpPost("SendReportByEmail/{idReport}")]
        public async Task<IActionResult> SendReportByEmail(int idReport)
        {
            try
            {
                // Kiểm tra xem báo cáo (report) có tồn tại
                var report = await _context.Reports.FindAsync(idReport);

                if (report == null)
                {
                    return NotFound("Không tìm thấy báo cáo với idReport đã cung cấp.");
                }

                // Truy vấn danh sách email đã tồn tại trong bảng Account
                var existingEmails = await _context.Accounts
                    .Where(a => a.Status != 3) // Lọc tài khoản có trạng thái true (hoạt động)
                    .Select(a => a.Email)
                    .ToListAsync();

                if (existingEmails.Count == 0)
                {
                    return BadRequest("Không tìm thấy email trong bảng Account.");
                }

                // Gửi báo cáo đến danh sách email đã chọn
                foreach (var email in existingEmails)
                {
                    // Tạo và gửi báo cáo thông qua email
                    // Sử dụng thông tin từ report

                    // Ví dụ sử dụng thư viện gửi email (như MimeKit hoặc SendGrid)
                    // Thực hiện logic gửi email ở đây

                    // Ví dụ sử dụng thư viện MimeKit
                    //var message = new MimeMessage();
                    //message.From.Add(new MailboxAddress("Your Name", "hunglmhe151034@fpt.edu.vn"));
                    //message.To.Add(new MailboxAddress(email, email));
                    //message.Subject = "Báo cáo lỗi report Sofware!";
                    //message.Body = new TextPart("plain")
                    //{
                    //    Text = "Content of the Email:\n" + report.Description
                    //};

                    // Sử dụng thư viện gửi email để gửi thông báo
                    var smtpClient = new SmtpClient
                    {
                        Host = "smtp.gmail.com", // Điền host của máy chủ SMTP bạn đang sử dụng
                        Port = 587, // Điền cổng của máy chủ SMTP
                        Credentials = new NetworkCredential("hunglmhe151034@fpt.edu.vn", "ibpe vddw zuvd gxib"), // Thông tin xác thực tài khoản Gmail
                        EnableSsl = true // Sử dụng SSL
                    };

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("hunglmhe151034@fpt.edu.vn"),
                        Subject = "Báo cáo lỗi report Software!",
                        Body = "<html><body><h1>Báo cáo lỗi</h1><p>" + report.Description + "</p></body></html>",
                        IsBodyHtml = true // Đánh dấu email có chứa mã HTML
                    };

                    mailMessage.To.Add(email);

                    await smtpClient.SendMailAsync(mailMessage);
                }

                return Ok("Báo cáo đã được gửi thành công đến danh sách email đã chọn.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Đã xảy ra lỗi: " + ex.Message);
            }
        }


    }

}
