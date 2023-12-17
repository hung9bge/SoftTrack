using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using static SoftTrack.API.Controllers.ImageController;

namespace SoftTrackTest.imageTest
{
    [TestFixture]
    public class GetImagesByReportAsync
    {
        private ImageController _imageController;
        private soft_track5Context _softtrack5Context;
        private IConfiguration _configuration;
        [SetUp]
        public void Setup()
        {
            _softtrack5Context = new soft_track5Context();
            _imageController = new ImageController(_configuration, _softtrack5Context);
        }

        [Test]
        public async Task Test1()
        {
            // Arrange
            int id = 1;
            var result = await _imageController.GetImagesByReportAsync(id);
            if (result.Value == null)
            {
                Assert.IsNull(result.Value);
            }
            Assert.IsNotEmpty(result.Value);

        }
        [Test]
        public async Task Test2()
        {
            int id = 1000;
            var result = await _imageController.GetImagesByReportAsync(id);
            if (result.Value == null)
            {
                Assert.IsNull(result.Value);
            }
            Assert.IsNotEmpty(result.Value);
        }
    }
}