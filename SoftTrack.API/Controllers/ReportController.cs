using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.API.Models;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using System.Globalization;
using System.Net;
using System.Net.Mail;


namespace SoftTrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly soft_track5Context _context;
        public static IWebHostEnvironment _webHostEnvironment;

        public ReportController(soft_track5Context context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Reports
        [HttpGet("ReportAll")]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetReports()
        {      
           var reports = await _context.Reports
            .OrderBy(reports => reports.Status)                
            .OrderBy(reports => reports.StartDate)        
            .ToListAsync();

            var reportDtos = reports.Select(report => new ReportDto
            {
                ReportId = report.ReportId,
                AppId = report.AppId,
                EmailSend = _context.Accounts
                            .Where(acc => acc.AccId == report.CreatorID)
                            .Select(acc => acc.Email)
                            .FirstOrDefault(),
                EmailUpp = _context.Accounts
                            .Where(acc => acc.AccId == report.UpdaterID)
                            .Select(acc => acc.Email)
                            .FirstOrDefault(),
                Title = report.Title,
                Description = report.Description,
                Type = report.Type,
                Start_Date = report.StartDate.ToString("dd/MM/yyyy"),
                End_Date = report.EndDate?.ToString("dd/MM/yyyy"), 
                ClosedDate = report.ClosedDate?.ToString("dd/MM/yyyy"),
                Status = report.Status
            });

            return reportDtos.ToList();
        }
     
        // GET: api/Reports/5
        [HttpGet("ReportWith{id}")]
        public async Task<ActionResult<ReportDto>> GetReport(int id)
        {
            var report = await _context.Reports
            .FirstOrDefaultAsync(x => x.ReportId == id);

            if (report == null)
            {
                return NotFound();
            }

            // Ánh xạ dữ liệu từ đối tượng Report sang đối tượng ReportDto
            var reportDto = new ReportDto
            {
                ReportId = report.ReportId,
                AppId = report.AppId,
                EmailSend = _context.Accounts
                            .Where(acc => acc.AccId == report.CreatorID)
                            .Select(acc => acc.Email)
                            .FirstOrDefault(),
                EmailUpp = _context.Accounts
                            .Where(acc => acc.AccId == report.UpdaterID)
                            .Select(acc => acc.Email)
                            .FirstOrDefault(),
                Title = report.Title,
                Description = report.Description,
                Type = report.Type,
                Start_Date = report.StartDate.ToString("dd/MM/yyyy"),
                End_Date = report.EndDate?.ToString("dd/MM/yyyy"),
                ClosedDate = report.ClosedDate?.ToString("dd/MM/yyyy"),
                Status = report.Status
            };

            return reportDto;
        }
        [HttpGet("ReportsByType/{type}")]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetReportsByType(string type)
        {
            var reports = await _context.Reports
               .Where(report => report.Type == type)
               .OrderBy(reports => reports.Status)
               .OrderBy(reports => reports.StartDate)
               .Select(report => new ReportDto
                {
                    ReportId = report.ReportId,
                    AppId = report.AppId,
                    EmailSend = _context.Accounts
                            .Where(acc => acc.AccId == report.CreatorID)
                            .Select(acc => acc.Email)
                            .FirstOrDefault(),
                   EmailUpp = _context.Accounts
                            .Where(acc => acc.AccId == report.UpdaterID)
                            .Select(acc => acc.Email)
                            .FirstOrDefault(),
                   Title = report.Title,
                    Description = report.Description,
                    Type = report.Type,
                    Start_Date = report.StartDate.ToString("dd/MM/yyyy"),
                    End_Date = report.EndDate.HasValue ? report.EndDate.Value.ToString("dd/MM/yyyy") : null,
                   ClosedDate = report.ClosedDate.HasValue ? report.ClosedDate.Value.ToString("dd/MM/yyyy") : null,
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
                    EmailSend = _context.Accounts
                            .Where(acc => acc.AccId == report.CreatorID)
                            .Select(acc => acc.Email)
                            .FirstOrDefault(),
                    EmailUpp = _context.Accounts
                            .Where(acc => acc.AccId == report.UpdaterID)
                            .Select(acc => acc.Email)
                            .FirstOrDefault(),
                    Title = report.Title,   
                    Description = report.Description,
                    Type = report.Type,
                    Start_Date = report.StartDate.ToString("dd/MM/yyyy"),
                    End_Date = report.EndDate.HasValue ? report.EndDate.Value.ToString("dd/MM/yyyy") : null,
                    ClosedDate = report.ClosedDate.HasValue ? report.ClosedDate.Value.ToString("dd/MM/yyyy") : null,
                    Status = report.Status

                })
                .ToListAsync();

            return reportsForSoftware;
        }
        //[HttpPost("CreateReport_os")]
        //public async Task<IActionResult> CreateReportByOs([FromForm] ReportOsModel reportModel)
        //{
        //    if (ModelState.IsValid)
        //    {   
        //        var applications = await _context.Applications
        //        .Where(app => (app.Os == reportModel.Os && app.Osversion == reportModel.OsVersion))
        //        .Select(app => app.AppId)
        //        .ToListAsync();

        //        string dateString = DateTime.Now.ToString("dd/MM/yyyy");

        //        List<int> lstReportId = new List<int>();

        //        foreach (var appid in applications)
        //        { 
        //            var newReport = new Report
        //            {

        //                AppId = appid,
        //                Title = reportModel.Title,
        //                Description = reportModel.Description,
        //                Type = reportModel.Type,
        //                Status = reportModel.Status

        //            };

        //            if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
        //            {
        //                newReport.StartDate = parsedDate;
        //            }
        //            if (DateTime.TryParseExact(reportModel.End_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate1))
        //            {
        //                newReport.EndDate = parsedDate1;
        //            }

        //            _context.Reports.Add(newReport);
        //            await _context.SaveChangesAsync();

        //            lstReportId.Add(newReport.ReportId);

        //        }


        //        if (reportModel.Images != null)
        //        {
        //            string path = _webHostEnvironment.WebRootPath + "\\images\\";
        //            if (!Directory.Exists(path))
        //            {
        //                Directory.CreateDirectory(path);
        //            }

        //            foreach (var file in reportModel.Images)
        //            {
        //                if (file.FileName == null)
        //                    continue;
        //                var pathImage = await UploadImage(path, file);
        //                foreach (var id in lstReportId)
        //                {
        //                    var img = new Image()
        //                    {
        //                        ReportId = id,
        //                        Image1 = pathImage
        //                    };
        //                    if (img != null)
        //                    {
        //                        _context.Images.Add(img);

        //                    }
        //                }
        //            }
        //        }

        //        //_context.Reports.Add(newReport);
        //        await _context.SaveChangesAsync();

        //        return Ok();
        //    }
        //    return BadRequest(ModelState);
        //}

        //[HttpPost("CreateReport_appids")]
        //public async Task<IActionResult> CreateReportByAppid([FromForm] ReportModel reportModel)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        string dateString = DateTime.Now.ToString("dd/MM/yyyy");

        //        var newReport = new Report
        //        {

        //            AppId = reportModel.AppId,
        //            Title = reportModel.Title,
        //            Description = reportModel.Description,
        //            Type = reportModel.Type,
        //            Status = reportModel.Status

        //        };

        //        if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
        //        {
        //            newReport.StartDate = parsedDate;
        //        }
        //        if (DateTime.TryParseExact(reportModel.End_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate1))
        //        {
        //            newReport.EndDate = parsedDate1;
        //        }

        //        _context.Reports.Add(newReport);
        //        //await _context.SaveChangesAsync();

        //        if (reportModel.Images != null)
        //        {
        //            string path = _webHostEnvironment.WebRootPath + "\\images\\";
        //            if (!Directory.Exists(path))
        //            {
        //                Directory.CreateDirectory(path);
        //            }

        //            foreach (var file in reportModel.Images)
        //            {
        //                if (file.FileName == null)
        //                    continue;
        //                var img = new Image()
        //                {
        //                    ReportId = newReport.ReportId,
        //                    Image1 = await UploadImage(path, file)
        //                };
        //                if (img != null)
        //                {
        //                    _context.Images.Add(img);

        //                }
        //            }
        //        }

        //        //_context.Reports.Add(newReport);
        //        await _context.SaveChangesAsync();

        //        return Ok("Report đã được thêm thành công.");
        //    }
        //    return BadRequest(ModelState);
        //}

        [HttpPost("CreateReport_appids")]
        public async Task<IActionResult> CreateReportByAppid( [FromForm] ReportCreateDto reportModel)
        {
            if (ModelState.IsValid)
            {
                if (reportModel.AppIds == null || !reportModel.AppIds.Any())
            {
                return BadRequest("Danh sách ID ứng dụng không hợp lệ.");
            }

            string dateString = DateTime.Now.ToString("dd/MM/yyyy");
                foreach (var appId in reportModel.AppIds)
                {
                    var newReport = new Report
                    {
                        AppId = appId,
                        CreatorID = reportModel.CreatorID,
                        Title = reportModel.Title,
                        Description = reportModel.Description,
                        Type = reportModel.Type,
                        Status = reportModel.Status
                        
                    };

                    if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                    {
                        newReport.StartDate = parsedDate;
                    }
                    if (DateTime.TryParseExact(reportModel.End_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate1))
                    {
                        newReport.EndDate = parsedDate1;
                    }
                    _context.Reports.Add(newReport);
                    await _context.SaveChangesAsync();
                    //lấy email PO quản lý app
                    var account = _context.Applications
                   .Where(app => app.AppId == newReport.AppId)
                   .Select(app => app.Acc)
                   .FirstOrDefault();
                    //lấy email người tạo report quản lý app
                    //var accountsend = await _context.Accounts.FindAsync(newReport.AccId);
                    var appPO = await _context.Applications.FindAsync(newReport.AppId);
                    // Sử dụng thư viện gửi email để gửi thông báo
                    var smtpClient = new SmtpClient
                    {
                        Host = "smtp.gmail.com", // Điền host của máy chủ SMTP bạn đang sử dụng
                        Port = 587, // Điền cổng của máy chủ SMTP
                        Credentials = new NetworkCredential("softtrackfpt@gmail.com", "aaje cjac hacp psjp"), // Thông tin xác thực tài khoản Gmail
                        EnableSsl = true // Sử dụng SSL
                    };  

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("softtrackfpt@gmail.com"),
                        Subject = "Báo cáo lỗi "+ newReport.Title+"!",
                        Body = "<html><body><h1>Báo cáo lỗi</h1><p>"+ "Tên app:" + appPO.Name +
                        "Thông tin lỗi:"+newReport.Description + "</p></body></html>",
                        IsBodyHtml = true // Đánh dấu email có chứa mã HTML
                    };  
                    mailMessage.To.Add(account.Email);
                    await smtpClient.SendMailAsync(mailMessage);

                    if (reportModel.Images != null)
                    {
                        string path = _webHostEnvironment.WebRootPath + "\\images\\";
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        foreach (var file in reportModel.Images)
                        {
                            if (file.FileName == null)
                                continue;
                            var img = new Image()
                            {
                                ReportId = newReport.ReportId,
                                Image1 = await UploadImage(path, file)
                            };
                            if (img != null)
                            {
                                _context.Images.Add(img);

                            }
                        }
                    }                
                }
                return Ok("Report đã được thêm thành công.");
            }
            return BadRequest(ModelState);
        }
        private async Task<string> UploadImage(string path, IFormFile file)
        {
            string filename = Guid.NewGuid().ToString() + "_" + file.FileName;
            path += filename;

            await file.CopyToAsync(new FileStream(path, FileMode.Create));

            return filename;
        }

        // PUT: api/Reports/5
        [HttpPut("UpdateReport/{id}")]
        public async Task<IActionResult> UpdateReport(int id, [FromForm] ReportUpdateDto reportModel)
        {
            if (reportModel == null)
                return null;
            string dateString = DateTime.Now.ToString("dd/MM/yyyy");
            // lấy dữ liệu report cần update
            var existingReport = await _context.Reports.FindAsync(id);
            // lấy dữ liệu accpunt người tạo report 
            var account_CR_report = await _context.Accounts.FindAsync(existingReport.CreatorID);
            // lấy dữ liệu accpunt người update report 
            var account_UP_report = await _context.Accounts.FindAsync(existingReport.UpdaterID);
            //lấy account quản lý app
            var account = _context.Applications
             .Where(app => app.AppId == existingReport.AppId)
             .Select(app => app.Acc)
             .FirstOrDefault();

            if (existingReport == null)
            {
                return NotFound();
            }

            if (reportModel.AppId != 0)
            {
                existingReport.AppId = (int)reportModel.AppId;
            }
            if (reportModel.Title != null && reportModel.Title != "string")
            {
                existingReport.Title = reportModel.Title;
            }
            if (reportModel.Description != null && reportModel.Description != "string")
            {
                existingReport.Description = reportModel.Description;
            }

            if (reportModel.Type != null && reportModel.Type != "string")
            {
                existingReport.Type = reportModel.Type;
            }

            if (!string.IsNullOrEmpty(reportModel.Start_Date))
            {
                if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                {
                    existingReport.StartDate = parsedDate;
                }
            }

            if (!string.IsNullOrEmpty(reportModel.End_Date))
            {
                if (DateTime.TryParseExact(reportModel.End_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                {
                    existingReport.EndDate = parsedDate;
                }
            }

            if (reportModel.Status != 0 && existingReport.Status != reportModel.Status)
            {
                existingReport.Status = reportModel.Status;
                if (existingReport.Status == 2)
                {                   
                   existingReport.ClosedDate = DateTime.Now;                
                }
                //lấy account người update report 
                var accountsend = await _context.Accounts.FindAsync(reportModel.UpdaterID);
                //lấy thông tin app trong report
                var appPO = await _context.Applications.FindAsync(existingReport.AppId);
                // Sử dụng thư viện gửi email để gửi thông báo
                var smtpClient = new SmtpClient
                {
                    Host = "smtp.gmail.com", // Điền host của máy chủ SMTP bạn đang sử dụng
                    Port = 587, // Điền cổng của máy chủ SMTP
                    Credentials = new NetworkCredential("softtrackfpt@gmail.com", "aaje cjac hacp psjp"), // Thông tin xác thực tài khoản Gmail
                    EnableSsl = true // Sử dụng SSL
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(accountsend.Email),
                    Subject = "Báo cáo lỗi " + existingReport.Title + "!",
                    Body = "<html><body><h1>Update báo cáo lỗi</h1><p>" + "Tên app:" + appPO.Name +
                    "Thông tin lỗi:" + existingReport.Description + "</p></body></html>",
                    IsBodyHtml = true // Đánh dấu email có chứa mã HTML
                };
                         
                if(account_UP_report == null)
                {
                    //TH: nếu acc update khác acc PO và acc update khác acc tạo report 
                    if (accountsend.Email != account.Email && accountsend.Email != account_CR_report.Email )
                    {
                        mailMessage.To.Add(account.Email);
                        mailMessage.To.Add(account_CR_report.Email);
                    }
                    //TH: nếu acc update là acc PO => gửi cho acc tạo feedback                
                    if (accountsend.Email == account.Email)
                    {
                        mailMessage.To.Add(account_CR_report.Email);
                    }
                    //TH: nếu acc uppdate là acc tạo report thì gủi mail cho PO
                    if (accountsend.Email == account_CR_report.Email)
                    {
                        mailMessage.To.Add(account.Email);
                    }

                    existingReport.UpdaterID = reportModel.UpdaterID;
                }
                if (account_UP_report != null)
                {
                    if (accountsend.Email != account.Email && accountsend.Email != account_CR_report.Email && accountsend.Email == account_UP_report.Email)
                    {
                        mailMessage.To.Add(account.Email);
                        mailMessage.To.Add(account_CR_report.Email);
                    }
                    if (accountsend.Email != account.Email && accountsend.Email != account_CR_report.Email && accountsend.Email != account_UP_report.Email)
                    {
                        mailMessage.To.Add(account_UP_report.Email);
                        mailMessage.To.Add(account.Email);
                        mailMessage.To.Add(account_CR_report.Email);
                    }

                    if (accountsend.Email == account.Email && accountsend.Email != account_UP_report.Email)
                    {
                        mailMessage.To.Add(account_UP_report.Email);
                        mailMessage.To.Add(account_CR_report.Email);
                    }
                    if (accountsend.Email == account.Email && accountsend.Email == account_UP_report.Email)
                    {
                        mailMessage.To.Add(account_CR_report.Email);
                    }
                    // nếu acc uppdate là acc tạo report thì gủi mail cho PO
                    if (accountsend.Email == account_CR_report.Email && accountsend.Email != account_UP_report.Email)
                    {
                        mailMessage.To.Add(account_UP_report.Email);
                        mailMessage.To.Add(account.Email);
                    }
                    if (accountsend.Email == account_CR_report.Email && accountsend.Email == account_UP_report.Email)
                    {
                        mailMessage.To.Add(account.Email);
                    }

                }
                if (existingReport.CreatorID != reportModel.UpdaterID || existingReport.UpdaterID != reportModel.UpdaterID)
                {
                    existingReport.UpdaterID = reportModel.UpdaterID;
                }
                if (existingReport.CreatorID == reportModel.UpdaterID)
                {
                    existingReport.UpdaterID = existingReport.CreatorID;
                }

                await smtpClient.SendMailAsync(mailMessage);
            }
            else
            {
                // acc tạo khác acc update
                if (existingReport.CreatorID != reportModel.UpdaterID || existingReport.UpdaterID != reportModel.UpdaterID)
                {
                    existingReport.UpdaterID = reportModel.UpdaterID;
                }
                if (existingReport.CreatorID == reportModel.UpdaterID)
                {
                    existingReport.UpdaterID = existingReport.CreatorID;
                }
             

            }
            _context.Reports.Update(existingReport);
            await _context.SaveChangesAsync();

            var lst = await _context.Images
                .Where(item => item.ReportId == id)
                .Select(item => new Image
                {
                    ImageId = item.ImageId,
                    ReportId = item.ReportId,
                    Image1 = item.Image1
                })
                .ToListAsync();

            if (lst.Any())
            {
                foreach (var item in lst)
                {
                    if (item != null)
                    {
                        _context.Images.Remove(item);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            if (reportModel.Images != null)
            {
                string path = _webHostEnvironment.WebRootPath + "\\images\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                foreach (var file in reportModel.Images)
                {
                    if (file.FileName == null)
                        continue;
                    var img = new Image()
                    {
                        ReportId = id,
                        Image1 = await UploadImage(path, file)
                    };
                    if (img != null)
                    {
                        _context.Images.Add(img);

                    }
                }

                await _context.SaveChangesAsync();
            }

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
                return Ok("Report đã được xóa thành công.");
            }
            return NotFound();
        }

        [HttpGet("list_reports_by_account/{accountId}")]
        public async Task<IActionResult> GetReportsForAccountAsync(int accountId)
        {
            var reports = _context.Reports
                .Where(r => r.App.AccId == accountId)
                .OrderBy(reports => reports.Status)
                .OrderBy(reports => reports.StartDate)
                .Select(report => new ReportDto
            {
                ReportId = report.ReportId,
                AppId = report.AppId,
                Title= report.Title,
                Description = report.Description,
                Type = report.Type,
                Start_Date = report.StartDate.ToString("dd/MM/yyyy"),
                End_Date = report.EndDate.HasValue ? report.EndDate.Value.ToString("dd/MM/yyyy") : null,
                Status = report.Status
            }).ToList();

            return Ok(reports);
        }
        //[HttpPost("SendReportByEmail/{idReport}")]
        //public async Task<IActionResult> SendReportByEmail(int idReport)
        //{
        //    try
        //    {
        //        // Kiểm tra xem báo cáo (report) có tồn tại
        //        var report = await _context.Reports.FindAsync(idReport);

        //        if (report == null)
        //        {
        //            return NotFound("Không tìm thấy báo cáo với idReport đã cung cấp.");
        //        }
            
        //        // Truy vấn danh sách email đã tồn tại trong bảng Account
        //        //var existingEmails = await _context.Accounts
        //        //    .Where(a => a.Status != 3) // Lọc tài khoản có trạng thái true (hoạt động)
        //        //    .Select(a => a.Email)
        //        //    .ToListAsync();
        //        // Truy email từ bảng "Account" mà các "Asset" của email đó có chứa "App" trong "Report" theo "ReportId," 
        //        var existingEmails = await _context.Accounts
        //        .Where(account => account.Applications
        //            .Any(app => app.AssetApplications
        //                .Any(assetApp => assetApp.AppId == report.AppId)))
        //        .Select(account => account.Email)
        //        .ToListAsync();

        //        if (existingEmails.Count == 0)
        //        {
        //            return BadRequest("Không tìm thấy email trong bảng Account.");
        //        }

        //        // Gửi báo cáo đến danh sách email đã chọn
        //        foreach (var email in existingEmails)
        //        {

        //            // Sử dụng thư viện gửi email để gửi thông báo
        //            var smtpClient = new SmtpClient
        //            {
        //                Host = "smtp.gmail.com", // Điền host của máy chủ SMTP bạn đang sử dụng
        //                Port = 587, // Điền cổng của máy chủ SMTP
        //                Credentials = new NetworkCredential("hunglmhe151034@fpt.edu.vn", "ibpe vddw zuvd gxib"), // Thông tin xác thực tài khoản Gmail
        //                EnableSsl = true // Sử dụng SSL
        //            };

        //            var mailMessage = new MailMessage
        //            {
        //                From = new MailAddress("hunglmhe151034@fpt.edu.vn"),
        //                Subject = "Báo cáo lỗi report Software!",
        //                Body = "<html><body><h1>Báo cáo lỗi</h1><p>" + report.Description + "</p></body></html>",
        //                IsBodyHtml = true // Đánh dấu email có chứa mã HTML
        //            };

        //            mailMessage.To.Add(email);

        //            await smtpClient.SendMailAsync(mailMessage);
        //        }

        //        return Ok("Báo cáo đã được gửi thành công đến danh sách email đã chọn.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "Đã xảy ra lỗi: " + ex.Message);
        //    }
        //}


    }

}
