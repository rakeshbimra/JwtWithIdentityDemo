using JwtWithIdentityDemo.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Infrastructure.Tests.Authentication
{
    [TestClass]
    public class UserManagerWrapperTests
    {
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private UserManagerWrapper _userManagerWrapper;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockUserManager = new Mock<UserManager<IdentityUser>>(
                Mock.Of<IUserStore<IdentityUser>>(),
                null, null, null, null, null, null, null, null
            );
            _userManagerWrapper = new UserManagerWrapper(_mockUserManager.Object);
        }

        [TestMethod]
        public async Task CreateUserAsync_Returns_IdentityResult()
        {
            // Arrange
            var user = new IdentityUser();
            string password = "password";
            var identityResult = IdentityResult.Success;
            _mockUserManager.Setup(m => m.CreateAsync(user, password)).ReturnsAsync(identityResult);

            // Act
            var result = await _userManagerWrapper.CreateUserAsync(user, password);

            // Assert
            Assert.AreEqual(identityResult, result);
        }

        [TestMethod]
        public async Task FindByNameAsync_Returns_IdentityUser()
        {
            // Arrange
            string userName = "testuser@test.com";
            var identityUser = new IdentityUser();
            _mockUserManager.Setup(m => m.FindByNameAsync(userName)).ReturnsAsync(identityUser);

            // Act
            var result = await _userManagerWrapper.FindByNameAsync(userName);

            // Assert
            Assert.AreEqual(identityUser, result);
        }

        [TestMethod]
        public async Task CheckPasswordAsync_Returns_True_When_Password_Is_Correct()
        {
            // Arrange
            var user = new IdentityUser();
            string password = "password";
            _mockUserManager.Setup(m => m.CheckPasswordAsync(user, password)).ReturnsAsync(true);

            // Act
            var result = await _userManagerWrapper.CheckPasswordAsync(user, password);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
