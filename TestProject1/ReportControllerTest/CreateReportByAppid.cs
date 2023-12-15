using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;

namespace SoftTrackTest.ReportControllerTest
{
    public class CreateReportByAppid
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
            // Arrange       
        ReportCreateDto reportCreate = new ReportCreateDto()
        {
            AppIds = new List<int> { 1 },
            AppId =  0,
            CreatorID = 1,
            Title = "Update login feedback",
            Description = "Update login feedback",
            Type = "Feedback",
            Start_Date = "05/12/2023",
            End_Date =  "08/12/2023",
            ClosedDate = null,
            Status = 1
         };
            var result = await reportController.CreateReportByAppid(reportCreate);
            Assert.IsInstanceOf<OkResult>(result);
        }
        [Test]
        public async Task Test2()
        {
        // Arrange       
        ReportCreateDto reportCreate = new ReportCreateDto()
        {
            AppIds = new List<int> { 1,2,3 },
            AppId = 0,
            CreatorID = 1,
            Title = "Update login feedback",
            Description = "Update login feedback",
            Type = "Feedback",
            Start_Date = "05/12/2023",
            End_Date = "08/12/2023",
            ClosedDate = null,
            Status = 1
        };
        var result = await reportController.CreateReportByAppid(reportCreate);
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task Test3()
        {
            // Arrange       
            ReportCreateDto reportCreate = new ReportCreateDto()
            {
                AppIds = new List<int> { 1000 },
                AppId = 0,
                CreatorID = 1,
                Title = "Update login feedback",
                Description = "Update login feedback",
                Type = "Feedback",
                Start_Date = "05/12/2023",
                End_Date = "08/12/2023",
                ClosedDate = null,
                Status = 1
            };
            var result = await reportController.CreateReportByAppid(reportCreate);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
