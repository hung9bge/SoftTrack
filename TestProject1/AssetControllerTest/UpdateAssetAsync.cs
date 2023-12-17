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
    public class UpdateAssetAsync
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
            var id = 1;
            var item = new AssetUpdateDto()
            {
                Name = "string",
                Cpu = "string",
                Gpu = "string",
                Ram = "string",
                Memory = "string",
                Os = "string",
                Version = "string",
                IpAddress = "string",
                Bandwidth = "string",
                Manufacturer = "string",
                Model = "string",
                SerialNumber = "string",
                Status = 1
            };

            var result = await _assetController.UpdateAssetAsync(id, item);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test2()
        {
            var id = 1;
            var item = new AssetUpdateDto()
            {
                Name = "Database Server 2",
                Cpu = "Intel Xeon E5520",
                Gpu = "Intel(R) UHD Graphics 620 NVIDIA GeForce MX250",
                Ram = "32",
                Memory = "2000",
                Os = "Window 10 Home",
                Version = "",
                IpAddress = "192.168.101,20",
                Bandwidth = "200Mbps",
                Manufacturer = "Asus",
                Model = "Asus Z10PE-D16 WS",
                SerialNumber = "23S1008AC14G",
                Status = 1
            };

            var result = await _assetController.UpdateAssetAsync(id, item);
            Assert.IsInstanceOf<OkResult>(result);
        }
        [Test]
        public async Task Test3()
        {
            var id = 1111;
            var item = new AssetUpdateDto()
            {
                Name = "Database Server 2",
                Cpu = "Intel Xeon E5520",
                Gpu = "Intel(R) UHD Graphics 620 NVIDIA GeForce MX250",
                Ram = "32",
                Memory = "2000",
                Os = "Window 10 Home",
                Version = "",
                IpAddress = "192.168.101,20",
                Bandwidth = "200Mbps",
                Manufacturer = "Asus",
                Model = "Asus Z10PE-D16 WS",
                SerialNumber = "23S1008AC14G",
                Status = 1
            };

            var result = await _assetController.UpdateAssetAsync(id, item);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}