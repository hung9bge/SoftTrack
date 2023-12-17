using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using static SoftTrack.API.Controllers.AssetController;

namespace SoftTrackTest.AssetTest
{
    [TestFixture]
    public class ListAllAssetsAsync
    {
        private AssetController _assetController;
        private soft_track5Context _softtrack5Context;
        private IConfiguration _configuration;
        [SetUp]
        public void Setup()
        {
            _softtrack5Context = new soft_track5Context();
            _assetController = new AssetController(_configuration, _softtrack5Context);
        }

        [Test]
        public async Task Test1()
        {
            // Arrange

            var result = await _assetController.ListAllAssetsAsync();
            if (result.Value == null)
            {
                Assert.IsNull(result.Value);
            }
            Assert.IsNotEmpty(result.Value);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            var mockAppController = new Mock<IAssetController>();
            mockAppController.Setup(x => x.ListAllAssetsAsync())
                             .ReturnsAsync((IEnumerable<AssetDto>)null);
            var _assetController = mockAppController.Object;

            // Act
            var result = await _assetController.ListAllAssetsAsync();

            // Assert
            Assert.IsNull(result);
        }
    }
}