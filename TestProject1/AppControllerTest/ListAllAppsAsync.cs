using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;

namespace SoftTrackTest.AppControllerTest

{
    [TestFixture]
    public class ListAllAppsAsync
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

            var result = await appController.ListAllAppsAsync();
            if (result.Value == null)
            {
                Assert.IsEmpty(result.Value);
            }
            Assert.IsNotEmpty(result.Value);

        }
    }
}