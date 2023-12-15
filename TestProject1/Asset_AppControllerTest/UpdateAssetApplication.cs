using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using SoftTrack.Manage.DTO.Asset_App;
using static SoftTrack.API.Controllers.AppController;

namespace SoftTrackTest.Asset_AppControllerTest
{
    public class UpdateAssetApplication
    {
        private Asset_AppController asset_AppController;
        private soft_track5Context _softtrack5Context;

        [SetUp]
        public void Setup()
        {
            _softtrack5Context = new soft_track5Context();
            asset_AppController = new Asset_AppController(_softtrack5Context);
        }

        [Test]
        public async Task Test1()
        {
            // Arrange
            int assetId = 1;
            int appId = 2;
            Asset_AppUpdateDto asset_app = new Asset_AppUpdateDto()
            {
                InstallDate = "22/12/2023",
                Status = 1
            };
            var result = await asset_AppController.UpdateAssetApplicationAsync(assetId, appId,asset_app);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            int assetId = 1000;
            int appId = 2;
            Asset_AppUpdateDto asset_app = new Asset_AppUpdateDto()
            {
                InstallDate = "22/12/2023",
                Status = 1
            };
            var result = await asset_AppController.UpdateAssetApplicationAsync(assetId, appId, asset_app);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test3()
        {
            // Arrange
            int assetId = 1;
            int appId = 2000;
            Asset_AppUpdateDto asset_app = new Asset_AppUpdateDto()
            {
                InstallDate = "22/12/2023",
                Status = 1
            };
            var result = await asset_AppController.UpdateAssetApplicationAsync(assetId, appId, asset_app);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test4()
        {
            // Arrange
            int assetId = 1000;
            int appId = 2000;
            Asset_AppUpdateDto asset_app = new Asset_AppUpdateDto()
            {
                InstallDate = "22/12/2023",
                Status = 1
            };
            var result = await asset_AppController.UpdateAssetApplicationAsync(assetId, appId, asset_app);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
    }
}
