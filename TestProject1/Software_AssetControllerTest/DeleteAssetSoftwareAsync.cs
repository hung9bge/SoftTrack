using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using SoftTrack.Manage.DTO.Asset_App;
using static SoftTrack.API.Controllers.AppController;

namespace SoftTrackTest.Software_AssetControllerTest
{
    public class DeleteAssetApplicationAsync
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
            int assetId = 1;
            int softwareId = 1;
            var result = await software_Asset.DeleteAssetSoftwareAsync(assetId, softwareId);
            Assert.IsInstanceOf<OkResult>(result);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            int assetId = 1000;
            int softwareId = 1;
            var result = await software_Asset.DeleteAssetSoftwareAsync(assetId, softwareId);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test3()
        {
            // Arrange
            int assetId = 1;
            int softwareId = 1000;
            var result = await software_Asset.DeleteAssetSoftwareAsync(assetId, softwareId);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test4()
        {
            // Arrange
            int assetId = 1000;
            int softwareId = 1000;
            var result = await software_Asset.DeleteAssetSoftwareAsync(assetId, softwareId);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
    }
}
