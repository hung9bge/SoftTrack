using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using static SoftTrack.API.Controllers.AppController;

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
            Assert.IsNotEmpty(result.Value);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            var mockAppController = new Mock<IAppController>();
            mockAppController.Setup(x => x.ListAllAppsAsync())
                             .ReturnsAsync((IEnumerable<ApplicationDto>)null);
            var appController = mockAppController.Object;

            // Act
            var result = await appController.ListAllAppsAsync();

            // Assert
            Assert.IsNull(result);
        }

    }
}