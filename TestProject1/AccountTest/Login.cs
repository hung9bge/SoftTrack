using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using System.Text;

namespace TestProject1.AcconutTest
{
    [TestFixture]
    public class Login
    {
        private AccountController _accountController;
        private soft_track5Context _softtrack5Context;
        private IConfiguration _configuration;
        [SetUp]
        public void Setup()
        {
            _softtrack5Context = new soft_track5Context();
            _accountController = new AccountController(_configuration, _softtrack5Context);
        }

        //[Test]
        //public async Task Test1()
        //{
        //    // Arrange
        //    var email = "hunglmhe151034@fpt.edu.vn";
        //    var configuration = new Mock<IConfiguration>();
        //    configuration.Setup(x => x["JwtIssuer"]).Returns("MyAPI");
        //    configuration.Setup(x => x["JwtKey"]).Returns("mySuperSecretKey123");

        //    var accountController = new AccountController(_configuration, _softtrack5Context);

        //    // Act
        //    var result = await accountController.Login(email);

        //    // Assert
        //    //Assert.IsInstanceOf<OkResult>(result);


        //}
        [Test]
        public async Task Test2()
        {
            // Arrange
            var email = "ahungthanhgank123@gmail.com";

            // Act
            var result = await _accountController.Login(email);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

    }
}