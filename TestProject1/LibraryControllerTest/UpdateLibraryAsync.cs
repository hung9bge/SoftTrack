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
    public class UpdateLibraryAsync
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
            int id = 1;
            var item = new LibraryUpdateDto()
            {
                LibraryId = 1,
                AppId = 1,
                Name = "string",
                Publisher = "string",
                LibraryKey = "58417bc4-4811-45fc-9b85",
                Start_Date = "15/11/2023",
                Time = 0,
                Status = 2
            };

            var result = await _libraryController.UpdateLibraryAsync(id, item);
            Assert.IsInstanceOf<OkResult>(result);

        }
        [Test]
        public async Task Test2()
        {
            int id = 0;
            var item = new LibraryUpdateDto()
            {
                LibraryId = 0,
                AppId = 0,
                Name = "string",
                Publisher = "string",
                LibraryKey = "string",
                Start_Date = "string",
                Time = 0,
                Status = 1
            };

            var result = await _libraryController.UpdateLibraryAsync(id, item);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
        [Test]
        public async Task Test3()
        {
            int id = 0;
            var item = new LibraryUpdateDto()
            {
                LibraryId = 0,
                AppId = 0,
                Name = "string",
                Publisher = "string",
                LibraryKey = "string",
                Start_Date = "string",
                Time = 0,
                Status = 2
            };

            var result = await _libraryController.UpdateLibraryAsync(id, item);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}