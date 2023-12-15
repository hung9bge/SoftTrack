using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;

namespace SoftTrackTest.AppControllerTest

{
    [TestFixture]
    public class CreateAppAsync
    {
        private AppController appController;
        private soft_track5Context _softtrack5Context;
        private IConfiguration _configuration;
        

        [SetUp]
        public void Setup()
        {
            _softtrack5Context = new soft_track5Context();
            appController = new AppController(_configuration, _softtrack5Context);
        }

        [Test]
        public async Task Test1()
        {
            // Arrange
            var id = 1;
            ApplicationCreateDto accountDto = new ApplicationCreateDto()
            {
                AccId = id,
                Name = "Books24x7",
                Publisher = "FPTU Library",
                Version = "12.2023",
                Release = "September 12, 2023",
                Type = "Web App",
                Os = "Window",
                Osversion = "10",
                Description = "Online book database was built to provide FPT University students with a place to store information and data about the school's courses.",
                Download = "Link download",
                Docs = "Link docs",
                Language = "C# .NET",
                Db = "SQL Server",
                Status = 1
            };
            var result = await appController.CreateAppAsync(accountDto);
            Assert.IsInstanceOf<OkResult>(result);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
        
            ApplicationCreateDto accountDto = new ApplicationCreateDto()
            {
                AccId = 0,
                Name = "Books24x7",
                Publisher = "FPTU Library",
                Version = "12.2023",
                Release = "September 12, 2023",
                Type = "Web App",
                Os = "Window",
                Osversion = "10",
                Description = "Online book database was built to provide FPT University students with a place to store information and data about the school's courses.",
                Download = "Link download",
                Docs = "Link docs",
                Language = "C# .NET",
                Db = "SQL Server",
                Status = 1
            };
            var result = await appController.CreateAppAsync(accountDto);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test3()
        {
            // Arrange

            ApplicationCreateDto accountDto = new ApplicationCreateDto()
            {
                AccId = 1,
                Name = "string",
                Publisher = "string",
                Version = "string",
                Release = "string",
                Type = "string",
                Os = "string",
                Osversion = "string",
                Description = "string",
                Download = "string",
                Docs = "string",
                Language = "string",
                Db = "string",
                Status = 1
            };
            var result = await appController.CreateAppAsync(accountDto);
            Assert.IsInstanceOf<OkResult>(result);

        }
    }

}