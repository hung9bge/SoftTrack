using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using static SoftTrack.API.Controllers.LicenseController;

namespace SoftTrackTest.LicenseTest
{
    [TestFixture]
    public class UpdateLicenseAsync
    {
        private LicenseController _licenseController;
        private soft_track5Context _softtrack5Context;
        private IConfiguration _configuration;
        [SetUp]
        public void Setup()
        {
            _softtrack5Context = new soft_track5Context();
            _licenseController = new LicenseController(_configuration, _softtrack5Context);
        }

        [Test]
        public async Task Test1()
        {
            // Arrange
            int id = 1;
            var item = new LicenseUpdateDto()
            {
                LicenseId = id,
                LicenseKey = "1E9D3A297F0C6859196F58ED3A90B51D859451CD",
                StartDate = "15/11/2023",
                Time = 5,
                Status = 2
            };

            var result = await _licenseController.UpdateLicenseAsync(id, item);
            Assert.IsInstanceOf<OkResult>(result);

        }
        [Test]
        public async Task Test2()
        {
            int id = 1;
            var item = new LicenseUpdateDto()
            {
                LicenseId = id,
                LicenseKey = "1E9D3A297F0C6859196F58ED3A90B51D859451CD",
                StartDate = "aabbcc",
                Time = 5,
                Status = 2
            };

            var result = await _licenseController.UpdateLicenseAsync(id, item);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
        [Test]
        public async Task Test3()
        {
            int id = 1000;
            var item = new LicenseUpdateDto()
            {
                LicenseId = id,
                LicenseKey = "1E9D3A297F0C6859196F58ED3A90B51D859451CD",
                StartDate = "15/11/2023",
                Time = 5,
                Status = 2
            };

            var result = await _licenseController.UpdateLicenseAsync(id, item);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
