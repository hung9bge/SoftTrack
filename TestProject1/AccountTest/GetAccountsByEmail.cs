using Microsoft.Extensions.Configuration;
using SoftTrack.API.Controllers;
using SoftTrack.Domain;

namespace SoftTrackTest.AccountTest
{
    [TestFixture]
    public class GetAccountsByEmail
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
            var email = "hunglmhe11111@fpt.edu.vn";
            var result = await _accountController.GetAccountsByEmail(email);
            Assert.IsNotEmpty(result.Value);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            var email = "hunglmhe2001@fpt.edu.vn";
            var result = await _accountController.GetAccountsByEmail(email);
            Assert.IsEmpty(result.Value);

        }
    }
}