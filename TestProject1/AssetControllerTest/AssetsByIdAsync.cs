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
    public class AssetsByIdAsync
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
            int id = 1;
            var result = await _assetController.AssetsByIdAsync(id);
            if (result.Value == null)
            {
                Assert.IsNull(result.Value);
            }
            Assert.IsNotNull(result.Value);

        }
        [Test]
        public async Task Test2()
        {
            int id = 1000;
            var result = await _assetController.AssetsByIdAsync(id);
            
            Assert.IsNull(result.Value);
        }
    }
}