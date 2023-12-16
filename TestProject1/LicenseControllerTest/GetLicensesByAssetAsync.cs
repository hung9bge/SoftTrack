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
    public class GetLicensesByAssetAsync
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
            var result = await _licenseController.GetLicensesByAssetAsync(id);

            Assert.IsNotNull(result.Value);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            int id = 1000;
            var result = await _licenseController.GetLicensesByAssetAsync(id);

            // Assert
            Assert.IsNull(result);
        }
    }
}