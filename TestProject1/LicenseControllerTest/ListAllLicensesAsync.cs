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
    public class ListAllLicensesAsync
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

            var result = await _licenseController.ListAllLicensesAsync();
            
            Assert.IsNotEmpty(result.Value);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            var mockAppController = new Mock<ILicenseController>();
            mockAppController.Setup(x => x.ListAllLicensesAsync())
                             .ReturnsAsync((IEnumerable<LicenseDto>)null);
            var _LicenseController = mockAppController.Object;

            // Act
            var result = await _LicenseController.ListAllLicensesAsync();

            // Assert
            Assert.IsNull(result);
        }
    }
}