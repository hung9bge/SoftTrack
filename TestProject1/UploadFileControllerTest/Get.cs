using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;

namespace SoftTrackTest.UploadFileControllerTest

{
    [TestFixture]
    public class Get
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
            string fileName = "image1.jpg";
            var result = await uploadController.Get(fileName);
            Assert.NotNull(result);

        }
        [Test]
        public async Task Test2()
        {
            string fileName = "image2.jpg";
            var result = await uploadController.Get(fileName);
            Assert.IsNull(result);

        }
    }

}