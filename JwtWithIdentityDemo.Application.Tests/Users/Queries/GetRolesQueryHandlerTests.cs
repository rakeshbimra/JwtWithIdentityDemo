using JwtWithIdentityDemo.Application.Abstractions.Authentication;
using JwtWithIdentityDemo.Application.Users.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Tests.Users.Queries
{
    [TestClass]
    public class GetRolesQueryHandlerTests
    {
        private Mock<IUserManagerWrapper> _mockUserManager;
        private Mock<ILogger<GetRolesQueryHandler>> _mockLogger;

        [TestInitialize]
        public void Initialize()
        {
            _mockUserManager = new Mock<IUserManagerWrapper>();
            _mockLogger = new Mock<ILogger<GetRolesQueryHandler>>();
        }

        [TestMethod]
        public async Task Handle_Returns_Expected_Result()
        {
            // Arrange
            var identityUser = new IdentityUser();
            var expectedRoles = new List<string> { "role1", "role2" };
            _mockUserManager.Setup(x => x.GetRolesAsync(identityUser))
                            .ReturnsAsync(expectedRoles);

            var handler = new GetRolesQueryHandler(_mockUserManager.Object, _mockLogger.Object);
            var query = new GetRolesQuery(identityUser);

            // Act
            var actualResult = await handler.Handle(query, CancellationToken.None);

            // Assert
            CollectionAssert.AreEqual(expectedRoles, actualResult.ToList());
        }
    }
}
