using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Moq;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using static SoftTrack.API.Controllers.RoleController;

namespace SoftTrackTest.RoleControllerTest
{
    public class GetRoles
    {
        private RoleController _roleController;
        private soft_track5Context _softtrack5Context;
        private IWebHostEnvironment _webHostEnvironment;
        [SetUp]
        public void Setup()
        {
            _softtrack5Context = new soft_track5Context();
            _roleController = new RoleController(_softtrack5Context);
        }

        [Test]
        public async Task Test1()
        {
            // Arrange
            var result = await _roleController.GetRoles();
            Assert.IsNotEmpty(result.Value);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            var mockAppController = new Mock<IRoleController>();
            mockAppController.Setup(x => x.GetRoles())
                             .ReturnsAsync((IEnumerable<Role>)null);
            var roleController = mockAppController.Object;

            // Act
            var result = await roleController.GetRoles();

            // Assert
            Assert.IsNull(result);

        }
    }
}
