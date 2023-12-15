using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;

namespace SoftTrackTest.ReportControllerTest
{
    public class UpdateReport
    {
        private ReportController reportController;
        private soft_track5Context _softtrack5Context;
        private IWebHostEnvironment _webHostEnvironment;
        [SetUp]
        public void Setup()
        {
            _softtrack5Context = new soft_track5Context();
            reportController = new ReportController(_softtrack5Context, _webHostEnvironment);
        }

        [Test]
        public async Task Test1()
        {
            var id = 41;
            // Arrange       
            ReportUpdateDto reportUpdate = new ReportUpdateDto()
            {
                AppId = 0,
                UpdaterID = 1,
                Title = "abc",
                Description = "abc",
                Type = "feedback",
                Start_Date = "string",
                End_Date = "string",
                ClosedDate = null,
                Status = 1
            };
            var result = await reportController.UpdateReport(id, reportUpdate);
            Assert.IsInstanceOf<OkResult>(result);
        }
        [Test]
        public async Task Test2()
        {
            var id = 1000;
            // Arrange       
            ReportUpdateDto reportUpdate = new ReportUpdateDto()
            {
                AppId = 0,
                UpdaterID = 1,
                Title = "abc",
                Description = "abc",
                Type = "feedback",
                Start_Date = "string",
                End_Date = "string",
                ClosedDate = null,
                Status = 1
            };
            var result = await reportController.UpdateReport(id, reportUpdate);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task Test3()
        {
            var id = 41;
            // Arrange       
            ReportUpdateDto reportUpdate = new ReportUpdateDto()
            {
                AppId = 0,
                UpdaterID = 100,
                Title = "abc",
                Description = "abc",
                Type = "feedback",
                Start_Date = "string",
                End_Date = "string",
                ClosedDate = null,
                Status = 1
            };
            var result = await reportController.UpdateReport(id, reportUpdate);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
