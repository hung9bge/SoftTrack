using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;

namespace SoftTrackTest.AppControllerTest

{
    [TestFixture]
    public class GetAppByIdAsync
    {
        private AppController appController;
        private soft_track5Context _softtrack5Context;
        private IConfiguration _configuration;
        [SetUp]
        public void Setup()
        {
            _softtrack5Context = new soft_track5Context();
            appController = new AppController(_configuration, _softtrack5Context);
        }

        [Test]
        public async Task Test1()
        {
            // Arrange
            var id = 1;
            var result = await appController.GetAppByIdAsync(id);
            Assert.NotNull(result.Value);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            var id = 123;
            var result = await appController.GetAppByIdAsync(id);

            Assert.IsNull(result.Value);

        }
    }

}