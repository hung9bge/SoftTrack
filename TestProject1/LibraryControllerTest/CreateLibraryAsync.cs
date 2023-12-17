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
    public class CreateLibraryAsync
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
            var item = new LibraryCreateDto()
            { 
                AppId = 2,
                Name = "Node.js",
                Publisher = "Node.js Foundation",
                LibraryKey = "AKNU443MM",
                Start_Date = "12/01/2023",
                Time = 6,
                Status = 1
            };

            var result = await _libraryController.CreateLibraryAsync(item);
            Assert.IsInstanceOf<OkResult>(result);

        }
        [Test]
        public async Task Test2()
        {
            var item = new LibraryCreateDto()
            {
                AppId = 10000,
                Name = "Node.js",
                Publisher = "Node.js Foundation",
                LibraryKey = "AKNU443MM",
                Start_Date = "12/01/2023",
                Time = 6,
                Status = 1
            };

            var result = await _libraryController.CreateLibraryAsync(item);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
        [Test]
        public async Task Test3()
        {
            var item = new LibraryCreateDto()
            {
                AppId = 2,
                Name = "Node.js",
                Publisher = "Node.js Foundation",
                LibraryKey = "AKNU443MM",
                Start_Date = "12/01/2023",
                Time = 6,
                Status = 1
            };
           
            var result = await _libraryController.CreateLibraryAsync(item);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}