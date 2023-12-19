using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using System.Collections.Generic;
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

        //// GET: api/Reports
        //[HttpGet("ReportAll")]
        //public async Task<ActionResult<IEnumerable<ReportDto>>> GetReports()
        //{      
        //   var reports = await _context.Reports
        //    .OrderBy(reports => reports.Status)                
        //    .OrderBy(reports => reports.StartDate)        
        //    .ToListAsync();

        //    var reportDtos = reports.Select(report => new ReportDto
        //    {
        //        ReportId = report.ReportId,
        //        AppId = report.AppId,
        //        EmailSend = _context.Accounts
        //                    .Where(acc => acc.AccId == report.CreatorID)
        //                    .Select(acc => acc.Email)
        //                    .FirstOrDefault(),
        //        EmailUpp = _context.Accounts
        //                    .Where(acc => acc.AccId == report.UpdaterID)
        //                    .Select(acc => acc.Email)
        //                    .FirstOrDefault(),
        //        Title = report.Title,
        //        Description = report.Description,
        //        Type = report.Type,
        //        Start_Date = report.StartDate.ToString("dd/MM/yyyy"),
        //        End_Date = report.EndDate?.ToString("dd/MM/yyyy"), 
        //        ClosedDate = report.ClosedDate?.ToString("dd/MM/yyyy"),
        //        Status = report.Status
        //    });

        //    return reportDtos.ToList();
        //}
     
        // GET: api/Reports/5
        //[HttpGet("ReportWith{id}")]
        //public async Task<ActionResult<ReportDto>> GetReport(int id)
        //{
        //    var report = await _context.Reports
        //    .FirstOrDefaultAsync(x => x.ReportId == id);

        //    if (report == null)
        //    {
        //        return NotFound();
        //    }

        //    // Ánh xạ dữ liệu từ đối tượng Report sang đối tượng ReportDto
        //    var reportDto = new ReportDto
        //    {
        //        ReportId = report.ReportId,
        //        AppId = report.AppId,
        //        EmailSend = _context.Accounts
        //                    .Where(acc => acc.AccId == report.CreatorID)
        //                    .Select(acc => acc.Email)
        //                    .FirstOrDefault(),
        //        EmailUpp = _context.Accounts
        //                    .Where(acc => acc.AccId == report.UpdaterID)
        //                    .Select(acc => acc.Email)
        //                    .FirstOrDefault(),
        //        Title = report.Title,
        //        Description = report.Description,
        //        Type = report.Type,
        //        Start_Date = report.StartDate.ToString("dd/MM/yyyy"),
        //        End_Date = report.EndDate?.ToString("dd/MM/yyyy"),
        //        ClosedDate = report.ClosedDate?.ToString("dd/MM/yyyy"),
        //        Status = report.Status
        //    };

        //    return reportDto;
        //}
        [HttpGet("ReportsByType/{type}")]
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetReportsByType(string type)
        {
            var reports = await _context.Reports
               .Where(report => report.Type == type)
               .OrderByDescending(reports => reports.StartDate)

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
        public async Task<ActionResult<IEnumerable<ReportDto>>> GetReportsForAppAndType(int AppId, string type)
        {
            var reportsForSoftware = await _context.Reports
                .Where(report => report.AppId == AppId && report.Type == type)
                  .OrderBy(reports => reports.Status)
               .ThenBy(reports => reports.StartDate)
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
            if (!reportsForSoftware.Any())
            {
                return NotFound();
            }
            return reportsForSoftware;
        }

        [HttpPost("CreateReport_appids")]
        public async Task<IActionResult> CreateReportByAppid( [FromForm] ReportCreateDto reportModel)
        {
            if (ModelState.IsValid)
            {
                if (reportModel.AppIds == null || !reportModel.AppIds.Any())
            {
                return NotFound();
            }

            string dateString = DateTime.Now.ToString("dd/MM/yyyy");
                foreach (var appId in reportModel.AppIds)
                {
                    var InputApp = await _context.Applications.FindAsync(appId);
                    if (InputApp == null)
                    {
                        return NotFound();
                    }
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
                        if (parsedDate1 > DateTime.Now)
                        {
                            newReport.EndDate = parsedDate1;
                        }
                        else
                        {
                            return NotFound();
                        }
                        
                    }
                    _context.Reports.Add(newReport);
                    await _context.SaveChangesAsync();
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
                        await _context.SaveChangesAsync();
                    }
                    //lấy email PO quản lý app
                    var account = _context.Applications
                   .Where(app => app.AppId == newReport.AppId)
                   .Select(app => app.Acc)
                   .FirstOrDefault();
                    //lấy email người tạo report quản lý app
                    var accountsend = await _context.Accounts.FindAsync(newReport.CreatorID);
                    var appPO = await _context.Applications.FindAsync(newReport.AppId);
                    // Sử dụng thư viện gửi email để gửi thông báo
                    var smtpClient = new SmtpClient
                    {
                        Host = "smtp.gmail.com", // Điền host của máy chủ SMTP bạn đang sử dụng
                        Port = 587, // Điền cổng của máy chủ SMTP
                        Credentials = new NetworkCredential("softtrackfpt@gmail.com", "aaje cjac hacp psjp"), // Thông tin xác thực tài khoản Gmail
                        EnableSsl = true // Sử dụng SSL
                    };

                    MailMessage mailMessage;
                    if (reportModel.Type == "Issue")
                    {
                        mailMessage = new MailMessage
                        {
                            From = new MailAddress("softtrackfpt@gmail.com"),
                            Subject = "Báo cáo lỗi " + newReport.Title + "!",
                            Body = "<html><body style='font-family: Arial, sans-serif;'>" +
                            "<h1 style='color: red;'>Thông tin chi tiết lỗi</h1>" +
                            "<p><strong>Tên app:</strong> " + appPO.Name + "</p>" +
                            "<p><strong>Người tạo:</strong> " + accountsend.Email + "</p>" +
                            "<p><strong>Mô tả:</strong> " + newReport.Description + "</p>" +
                            "<p><strong>Ngày khởi tạo:</strong> " + dateString + "</p>" +
                            "<hr/>" +
                            "<p style='font-size: 80%; color: #888;'>SoftTrack - Hệ thống quản lý lỗi</p>" +
                            "</body></html>",
                            IsBodyHtml = true // Đánh dấu email có chứa mã HTML
                        };
                    }
                    else
                    {
                        mailMessage = new MailMessage
                        {
                            From = new MailAddress("softtrackfpt@gmail.com"),
                            Subject = "Báo cáo phản hồi về: " + newReport.Title + "!",
                            Body = "<html><body style='font-family: Arial, sans-serif;'>" +
                            "<h1 style='color: red;'>Thông tin chi tiết phản hồi </h1>" +
                            "<p><strong>Tên app:</strong> " + appPO.Name + "</p>" +
                            "<p><strong>Người tạo:</strong> " + accountsend.Email + "</p>" +
                            "<p><strong>Mô tả:</strong> " + newReport.Description + "</p>" +
                            "<p><strong>Ngày khởi tạo:</strong> " + dateString + "</p>" +
                            "<hr/>" +
                            "<p style='font-size: 80%; color: #888;'>SoftTrack - Hệ thống quản lý lỗi</p>" +
                            "</body></html>",
                            IsBodyHtml = true // Đánh dấu email có chứa mã HTML
                        };
                    }
                    
                    foreach (var img in _context.Images.Where(i => i.ReportId == newReport.ReportId))
                    {
                     string path = _webHostEnvironment.WebRootPath + "\\images\\" + img.Image1;
                        Attachment attachment = new Attachment(path);
                        mailMessage.Attachments.Add(attachment);
                        //using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                        //{
                        //    Attachment attachment = new Attachment(fileStream, Path.GetFileName(path));
                        //    mailMessage.Attachments.Add(attachment);

                    }
                    mailMessage.To.Add(account.Email);
                    try
                    {
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                    finally
                    {
                        // Đóng tất cả các tệp tin đính kèm
                        foreach (Attachment attachment in mailMessage.Attachments)
                        {
                            attachment.Dispose();
                        }
                    }
                }
                return Ok();
            }
            return NotFound();
        }
        private async Task<string> UploadImage(string path, IFormFile file)
        {
            string filename = Guid.NewGuid().ToString() + "_" + file.FileName;
            path += filename;       
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
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
            if (existingReport == null)
            {
                return NotFound();
            }
            // lấy dữ liệu accpunt người tạo report 
            var account_CR_report = await _context.Accounts.FindAsync(existingReport.CreatorID);          
            // lấy dữ liệu accpunt người update có trong report         
            var account_UP_report = await _context.Accounts.FindAsync(existingReport.UpdaterID);
            // lấy dữ liệu accpunt người update report         
            var account_UP_newreport = await _context.Accounts.FindAsync(reportModel.UpdaterID);
            //lấy account quản lý app
            var account = _context.Applications
             .Where(app => app.AppId == existingReport.AppId)
             .Select(app => app.Acc)
             .FirstOrDefault();
         
            if (account_CR_report == null || account_UP_newreport == null || account == null )
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

            if (reportModel.Status != 0 && existingReport.Status != reportModel.Status)
            {
                existingReport.Status = reportModel.Status;
                if (existingReport.Status == 2)
                {
                    existingReport.ClosedDate = DateTime.Now;
                }
                if (existingReport.Status == 1)
                {
                    existingReport.ClosedDate = null;
                }

                _context.Reports.Update(existingReport);
                await _context.SaveChangesAsync();

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

                string stateMsg = "Cancel";
                switch (reportModel.Status)
                {
                    case 1:
                        stateMsg = "Unsolved";
                        break;
                    case 2:
                        stateMsg = "Solved";
                        break;
                    case 3:
                        stateMsg = "Deleted";
                        break;
                    final:
                        stateMsg = "Cancel";
                }
                MailMessage mailMessage;

                if (reportModel.Type == "Issue")
                {
                    mailMessage = new MailMessage
                    {
                        From = new MailAddress("softtrackfpt@gmail.com"),
                        Subject = "Báo cáo lỗi " + existingReport.Title + "!",
                        Body = "<html><body style='font-family: Arial, sans-serif;'>" +
                            "<h1 style='color: red;'>Thông tin chi tiết lỗi</h1>" +
                            "<p><strong>Tên app:</strong> " + account.Name + "</p>" +
                            "<p><strong>Người update:</strong> " + accountsend.Email + "</p>" +
                            "<p><strong>Trạng thái:</strong> " + stateMsg + "</p>" +
                            "<p><strong>Mô tả:</strong> " + existingReport.Description + "</p>" +
                            "<p><strong>Ngày khởi tạo:</strong> " + dateString + "</p>" +
                            "<hr/>" +
                            "<p style='font-size: 80%; color: #888;'>SoftTrack - Hệ thống quản lý lỗi</p>" +
                            "</body></html>",
                        IsBodyHtml = true // Đánh dấu email có chứa mã HTML
                    };
                }
                else
                {
                    mailMessage = new MailMessage
                    {
                        From = new MailAddress("softtrackfpt@gmail.com"),
                        Subject = "Báo cáo phản hồi về: " + existingReport.Title + "!",
                        Body = "<html><body style='font-family: Arial, sans-serif;'>" +
                            "<h1 style='color: red;'>Thông tin chi tiết phản hồi</h1>" +
                            "<p><strong>Tên app:</strong> " + account.Name + "</p>" +
                            "<p><strong>Người update:</strong> " + accountsend.Email + "</p>" +
                            "<p><strong>Trạng thái:</strong> " + stateMsg + "</p>" +
                            "<p><strong>Mô tả:</strong> " + existingReport.Description + "</p>" +
                            "<p><strong>Ngày khởi tạo:</strong> " + dateString + "</p>" +
                            "<hr/>" +
                            "<p style='font-size: 80%; color: #888;'>SoftTrack - Hệ thống quản lý lỗi</p>" +
                            "</body></html>",
                        IsBodyHtml = true // Đánh dấu email có chứa mã HTML
                    };
                }
                foreach (var img in _context.Images.Where(i => i.ReportId == existingReport.ReportId))
                {
                    string path = _webHostEnvironment.WebRootPath + "\\images\\" + img.Image1;
                    Attachment attachment = new Attachment(path);
                    mailMessage.Attachments.Add(attachment);
                    //using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    //{
                    //    Attachment attachment = new Attachment(fileStream, Path.GetFileName(path));
                    //    mailMessage.Attachments.Add(attachment);

                }
                mailMessage.To.Add(account.Email);

                if (account_UP_report == null)
                {
                    //TH: nếu acc update khác acc PO và acc update khác acc tạo report 
                    if (accountsend.Email != account.Email && accountsend.Email != account_CR_report.Email)
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
                mailMessage.To.Add(account.Email);
                if (reportModel.Type == "Issue" || (reportModel.Type == "Feedback" && reportModel.Status == 2))
                {
                    try
                    {
                            await smtpClient.SendMailAsync(mailMessage);
                    }
                    catch 
                    {
                        // Đóng tất cả các tệp tin đính kèm
                    }

                }
                foreach (Attachment attachment in mailMessage.Attachments)
                {
                    attachment.Dispose();
                }
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

            return Ok();
            
        }
        // DELETE: api/Reports/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Report>> DeleteReport(int id)
        //{
        //    var report = await _context.Reports.FindAsync(id);
        //    if (report != null)
        //    {
        //        report.Status = 3;
        //        await _context.SaveChangesAsync();
        //        return Ok("Report đã được xóa thành công.");
        //    }
        //    return NotFound();
        //}

    }

}
