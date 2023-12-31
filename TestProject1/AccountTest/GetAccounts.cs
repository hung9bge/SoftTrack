﻿using Microsoft.AspNetCore.Mvc;
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
    public class GetAccount
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
            
            var result = await _accountController.GetAccounts();
            if(result.Value == null)
            {
                Assert.IsNull(result.Value);
            }
            Assert.IsNotEmpty(result.Value);

        }
        [Test]
        public async Task Test2()
        {
            // Arrange
            var mockAppController = new Mock<IAccountController>();
            mockAppController.Setup(x => x.GetAccounts())
                             .ReturnsAsync((IEnumerable<AccountDto>)null);
            var _accountController = mockAppController.Object;

            // Act
            var result = await _accountController.GetAccounts();

            // Assert
            Assert.IsNull(result);
        }
    }
}