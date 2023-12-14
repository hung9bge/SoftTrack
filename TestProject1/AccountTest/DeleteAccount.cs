using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;
using SoftTrack.Manage.DTO;
using static SoftTrack.API.Controllers.AccountController;
using static SoftTrack.API.Controllers.AppController;

namespace SoftTrackTest.AccountTest
{
    [TestFixture]
    public class DeleteAccount
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
            int accountId = 1;
            var result = await _accountController.DeleteAccount(accountId);

            Assert.IsInstanceOf<NotFoundResult>(result);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            int accountId = 1000;
            var result = await _accountController.DeleteAccount(accountId);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
        [Test]
        public async Task Test3()
        {
            // Arrange
            int accountId = 2;
            var result = await _accountController.DeleteAccount(accountId);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}