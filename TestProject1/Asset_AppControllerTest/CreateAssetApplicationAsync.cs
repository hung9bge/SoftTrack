using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using static SoftTrack.API.Controllers.AppController;

namespace SoftTrackTest.Asset_AppControllerTest
{
    public class CreateAssetApplicationAsync
    {
        private Asset_AppController asset_AppController;
        private soft_track5Context _softtrack5Context;

        [SetUp]
        public void Setup()
        {
            _softtrack5Context = new soft_track5Context();
            asset_AppController = new Asset_AppController( _softtrack5Context);
        }

        [Test]
        public async Task Test1()
        {
            // Arrange
           
            AssetApplicationDTO asset_app = new AssetApplicationDTO()
            {        
              AssetId = 1,
              AppId = 6,
              InstallDate = "14/12/2023",
              Status = 1               
             };
            var result = await asset_AppController.CreateAssetApplicationAsync(asset_app);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            AssetApplicationDTO asset_app = new AssetApplicationDTO()
            {
                AssetId = 1,
                AppId = 1,
                InstallDate = "14/12/2023",
                Status = 1
            };
            var result = await asset_AppController.CreateAssetApplicationAsync(asset_app);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test3()
        {
            // Arrange
            AssetApplicationDTO asset_app = new AssetApplicationDTO()
            {
                AssetId = 0,
                AppId = 1,
                InstallDate = "14/12/2023",
                Status = 1
            };
            var result = await asset_AppController.CreateAssetApplicationAsync(asset_app);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test4()
        {
            // Arrange
            AssetApplicationDTO asset_app = new AssetApplicationDTO()
            {
                AssetId = 1,
                AppId = 0,
                InstallDate = "14/12/2023",
                Status = 1
            };
            var result = await asset_AppController.CreateAssetApplicationAsync(asset_app);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
    }
}
