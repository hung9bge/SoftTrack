using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;

namespace SoftTrackTest.UploadFileControllerTest

{
    [TestFixture]
    public class Post
    {
        private UploadFileController uploadController;
        private soft_track5Context _softtrack5Context;
        public static IWebHostEnvironment _webHostEnvironment;


        [SetUp]
        public void Setup()
        {
            _softtrack5Context = new soft_track5Context();
            uploadController = new UploadFileController(_webHostEnvironment, _softtrack5Context);
        }

        [Test]
        public async Task Test1()
        {
            // Arrange
            var item = new FileUpload()
            {
                files = null,
            };
            var result = await uploadController.Post(item);
            Assert.IsInstanceOf<NotFoundResult>(result);

        }
    }

}