using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using static SoftTrack.API.Controllers.LibraryController;

namespace SoftTrackTest.LibraryTest
{
    [TestFixture]
    public class ListAllLibrariesAsync
    {
        private LibraryController _libraryController;
        private soft_track5Context _softtrack5Context;
        private IConfiguration _configuration;
        [SetUp]
        public void Setup()
        {
            _softtrack5Context = new soft_track5Context();
            _libraryController = new LibraryController(_configuration, _softtrack5Context);
        }

        [Test]
        public async Task Test1()
        {
            // Arrange

            var result = await _libraryController.ListAllLibrariesAsync();
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
            var mockAppController = new Mock<ILibraryController>();
            mockAppController.Setup(x => x.ListAllLibrariesAsync())
                             .ReturnsAsync((IEnumerable<LibraryDto>)null);
            var _LibraryController = mockAppController.Object;

            // Act
            var result = await _LibraryController.ListAllLibrariesAsync();

            // Assert
            Assert.IsNull(result);
        }
    }
}