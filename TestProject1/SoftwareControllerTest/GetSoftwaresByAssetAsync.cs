using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;

namespace SoftTrackTest.SoftwareControllerTest

{
    [TestFixture]
    public class GetSoftwaresByAssetAsync
    {
        private SoftwareController softwareController;
        private soft_track5Context _softtrack5Context;
        private IConfiguration _configuration;


        [SetUp]
        public void Setup()
        {
            _softtrack5Context = new soft_track5Context();
            softwareController = new SoftwareController(_configuration, _softtrack5Context);
        }

        [Test]
        public async Task Test1()
        {
            // Arrange
            var id = 1;
            
            var result = await softwareController.GetSoftwaresByAssetAsync(id);
            Assert.IsNotEmpty(result.Value);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            var id = 1000;

            var result = await softwareController.GetSoftwaresByAssetAsync(id);
            Assert.IsNull(result.Value);

        }
        [Test]
        public async Task Test3()
        {
            // Arrange
            var id = 1000;

            var result = await softwareController.GetSoftwaresByAssetAsync(id);
            Assert.IsNull(result.Value);

        }
    }

}