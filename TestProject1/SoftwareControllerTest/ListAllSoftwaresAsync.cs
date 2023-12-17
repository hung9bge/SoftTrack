using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using static SoftTrack.API.Controllers.SoftwareController;

namespace SoftTrackTest.SoftwareControllerTest

{
    [TestFixture]
    public class ListAllSoftwaresAsync
    {
        private SoftwareController softwareController;
        private soft_track5Context _softtrack5Context;
        private IConfiguration _configuration;


        [SetUp]
        public void Setup()
        {
            _softtrack5Context = new soft_track5Context();
            softwareController = new SoftwareController(_configuration, _softtrack5Context);
        }

        [Test]
        public async Task Test1()
        {
            var result = await softwareController.ListAllSoftwaresAsync();
            Assert.IsNotEmpty(result.Value);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            var mockAppController = new Mock<ISoftwareController>();
            mockAppController.Setup(x => x.ListAllSoftwaresAsync())
                             .ReturnsAsync((IEnumerable<SoftwareDto>)null);
            var softwareController = mockAppController.Object;

            // Act
            var result = await softwareController.ListAllSoftwaresAsync();

            // Assert
            Assert.IsNull(result);

        }
    }

}