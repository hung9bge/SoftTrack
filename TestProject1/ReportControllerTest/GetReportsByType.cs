using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;


namespace SoftTrackTest.ReportControllerTest
{
    public class GetReportsByType
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
            string type = "issue";
            var result = await reportController.GetReportsByType(type);
            Assert.NotNull(result.Value);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            string type = "test";
            var result = await reportController.GetReportsByType(type);
            Assert.IsNull(result.Value);

        }
    }
}
