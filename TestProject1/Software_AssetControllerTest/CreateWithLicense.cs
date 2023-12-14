using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using SoftTrack.Manage.DTO.Asset_App;
using static SoftTrack.API.Controllers.AppController;

namespace SoftTrackTest.Software_AssetControllerTest
{
    public class CreateWithLicense
    {
        private Software_Asset software_Asset;
        private soft_track5Context _softtrack5Context;

        [SetUp]
        public void Setup()
        {
            _softtrack5Context = new soft_track5Context();
            software_Asset = new Software_Asset(_softtrack5Context);
        }

        [Test]
        public async Task Test1()
        {
            // Arrange
            LicenseDto license = new LicenseDto()
            {
              AssetId = 1,
              SoftwareId = 1,
              InstallDate = "string",
              Status_AssetSoftware = 1,
              LicenseKey = "string",
              Start_Date = "string",
              Time = 0,
              Status_License = 0
            };
            var result = await software_Asset.CreateWithLicense(license);
            Assert.IsInstanceOf<OkResult>(result);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            LicenseDto license = new LicenseDto()
            {
                AssetId = 1000,
                SoftwareId = 1,
                InstallDate = "string",
                Status_AssetSoftware = 1,
                LicenseKey = "string",
                Start_Date = "string",
                Time = 0,
                Status_License = 0
            };
            var result = await software_Asset.CreateWithLicense(license);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test3()
        {
            // Arrange
            LicenseDto license = new LicenseDto()
            {
                AssetId = 1,
                SoftwareId = 1000,
                InstallDate = "string",
                Status_AssetSoftware = 1,
                LicenseKey = "string",
                Start_Date = "string",
                Time = 0,
                Status_License = 0
            };
            var result = await software_Asset.CreateWithLicense(license);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test4()
        {
            // Arrange
            LicenseDto license = new LicenseDto()
            {
                AssetId = 1,
                SoftwareId = 2,
                InstallDate = "14/12/2023",
                Status_AssetSoftware = 1,
                LicenseKey = "abcde",
                Start_Date = "13/12/2023",
                Time = 1,
                Status_License = 1
            };
            var result = await software_Asset.CreateWithLicense(license);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test5()
        {
            // Arrange
            LicenseDto license = new LicenseDto()
            {
                AssetId = 1000,
                SoftwareId = 2,
                InstallDate = "14/12/2023",
                Status_AssetSoftware = 1,
                LicenseKey = "abcde",
                Start_Date = "13/12/2023",
                Time = 1,
                Status_License = 1
            };
            var result = await software_Asset.CreateWithLicense(license);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test6()
        {
            // Arrange
            LicenseDto license = new LicenseDto()
            {
                AssetId = 1,
                SoftwareId = 2000,
                InstallDate = "14/12/2023",
                Status_AssetSoftware = 1,
                LicenseKey = "abcde",
                Start_Date = "13/12/2023",
                Time = 1,
                Status_License = 1
            };
            var result = await software_Asset.CreateWithLicense(license);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test7()
        {
            // Arrange
            LicenseDto license = new LicenseDto()
            {
                AssetId = 1000,
                SoftwareId = 2000,
                InstallDate = "14/12/2023",
                Status_AssetSoftware = 1,
                LicenseKey = "abcde",
                Start_Date = "13/12/2023",
                Time = 1,
                Status_License = 1
            };
            var result = await software_Asset.CreateWithLicense(license);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
    }
}
