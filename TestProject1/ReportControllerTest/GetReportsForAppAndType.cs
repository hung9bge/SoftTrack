using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;


namespace SoftTrackTest.ReportControllerTest
{
    public class GetReportsForAppAndType
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
            int appId = 1;
            string type = "issue";
            var result = await reportController.GetReportsForAppAndType(appId,type);
            Assert.NotNull(result.Value);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            int appId = 1;
            string type = "test";
            var result = await reportController.GetReportsForAppAndType(appId, type);
            Assert.IsNull(result.Value);

        }
        [Test]
        public async Task Test3()
        {
            // Arrange
            int appId = 1000;
            string type = "issue";
            var result = await reportController.GetReportsForAppAndType(appId, type);
            Assert.IsNull(result.Value);

        }
    }
}
