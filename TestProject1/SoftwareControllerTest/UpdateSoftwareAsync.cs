using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;

namespace SoftTrackTest.SoftwareControllerTest

{
    [TestFixture]
    public class UpdateSoftwareAsync
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
            int id = 1;
            // Arrange
            SoftwareUpdateDto item = new SoftwareUpdateDto()
            {
                Name = "ClamAV Product",
                Publisher = "ClamAV Team",
                Version = "1.2.1",
                Release = "August 27, 2023",
                Type = "Antivirus",
                Os = "macOS",
                Status = 1
            };
            var result = await softwareController.UpdateSoftwareAsync(id, item);
            Assert.IsInstanceOf<OkResult>(result);

        }
        [Test]
        public async Task Test2()
        {
            int id = 1000;
            // Arrange
            SoftwareUpdateDto item = new SoftwareUpdateDto()
            {
                Name = "ClamAV Product",
                Publisher = "ClamAV Team",
                Version = "1.2.1",
                Release = "August 27, 2023",
                Type = "Antivirus",
                Os = "macOS",
                Status = 1
            };
            var result = await softwareController.UpdateSoftwareAsync(id, item);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test3()
        {
            int id = 1;
            // Arrange
            SoftwareUpdateDto item = new SoftwareUpdateDto()
            {
                Name = "ClamAV Product",
                Publisher = "ClamAV Team",
                Version = "1.2.1",
                Release = "abc",
                Type = "Antivirus",
                Os = "macOS",
                Status = 1
            };
            var result = await softwareController.UpdateSoftwareAsync(id, item);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
    }

}