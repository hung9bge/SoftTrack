using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;

namespace SoftTrackTest.SoftwareControllerTest

{
    [TestFixture]
    public class CreateSoftwareAsync
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
            // Arrange
            SoftwareCreateDto item = new SoftwareCreateDto()
            {
                Name = "ClamAV",
                Publisher = "ClamAV Team",
                Version = "1.2.1",
                Release = "August 27, 2023",
                Type = "Antivirus",
                Os = "macOS",
                Status = 1
            };
            var result = await softwareController.CreateSoftwareAsync(item);
            Assert.IsInstanceOf<OkResult>(result);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            SoftwareCreateDto item = new SoftwareCreateDto()
            {
                Name = "ClamAV",
                Publisher = "ClamAV Team",
                Version = "1.2.1",
                Release = "August 27, 2023",
                Type = "Antivirus",
                Os = "macOS",
                Status = 1
            };
            var result = await softwareController.CreateSoftwareAsync(item);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
    }

}