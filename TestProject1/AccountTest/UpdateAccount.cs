using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;

namespace SoftTrackTest.AccountTest
{
    [TestFixture]
    public class UpdateAccount
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
            var id = 5;
            AccountUpdateDto accountDto = new AccountUpdateDto(){
             Name = "hung",
              Email = "hunglmhe11111@fpt.edu.vn",
             Status = 1,
              RoleId = 1
                };

            var result = await _accountController.UpdateAccount(id, accountDto);

            Assert.IsInstanceOf<OkResult>(result);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange

            var id = 15;
            AccountUpdateDto accountDto = new AccountUpdateDto()
            {
                Name = "dung",
                Email = "abcdung123@fpt.edu.vn",
                Status = 1,
                RoleId = 1
            };

            var result = await _accountController.UpdateAccount(id, accountDto);

            Assert.IsInstanceOf<NotFoundResult>(result);

        }

    }
}
