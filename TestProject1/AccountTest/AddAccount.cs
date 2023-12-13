using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;

namespace SoftTrackTest.AccountTest
{
    [TestFixture]
    public class AddAccount
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
        [Test]
        public async Task Test1()
        {
            // Arrange
      
            AccountCreateDto accountDto = new AccountCreateDto()
            {
                Name = "hung",
                Email = "hunglm1111@fpt.edu.vn",
                Status = 1,
                RoleId = 2
            };

            var result = await _accountController.AddAccount( accountDto);

            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange

     
            AccountCreateDto accountDto = new AccountCreateDto()
            {
                Name = "hung",
                Email = "hunglm1111@fpt.edu.vn",
                Status = 1,
                RoleId = 2
            };

            var result = await _accountController.AddAccount( accountDto);

            Assert.IsInstanceOf<NotFoundResult>(result);

        }

    }
}
