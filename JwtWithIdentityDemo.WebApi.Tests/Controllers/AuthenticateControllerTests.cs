using JwtWithIdentityDemo.Application.Abstractions.Authentication;
using JwtWithIdentityDemo.Application.Users.Queries;
using JwtWithIdentityDemo.WebApi.Controllers;
using JwtWithIdentityDemo.WebApi.Models.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.WebApi.Tests.Controllers
{
    [TestClass]
    public class AuthenticateControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
        private AuthenticateController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mediatorMock = new Mock<IMediator>();
            _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            _controller = new AuthenticateController(_mediatorMock.Object, _jwtTokenGeneratorMock.Object);
        }

        [TestMethod]
        public async Task Login_Valid_Credentials_Returns_Token()
        {
            // Arrange
            var username = "testuser";
            var password = "testpassword";
            var user = new IdentityUser { Id = Guid.NewGuid().ToString(), UserName = username };
            var roles = new List<string> { "admin", "user" };
            var token = "testtoken";
            var expires = DateTime.UtcNow.AddDays(1);
            var model = new LoginModel { Username = username, Password = password };

             _mediatorMock.Setup(x => x.Send(It.IsAny<FindUserByNameQuery>(), default)).ReturnsAsync(user);
            _mediatorMock.Setup(x => x.Send(It.IsAny<CheckPasswordQuery>(), default)).ReturnsAsync(true);
            _mediatorMock.Setup(x => x.Send(It.IsAny<GetRolesQuery>(), default)).ReturnsAsync(roles);
            _jwtTokenGeneratorMock.Setup(x => x.GenerateToken(It.IsAny<Guid>(), username, roles)).Returns((token, expires));

           

            // Act
            var result = await _controller.Login(model);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var response = okResult.Value;

            // Convert the response object to a dictionary so we can access its properties
            var dict = response.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(response, null));

            // Assert that the token and expires properties are correct
            Assert.AreEqual(token, dict["token"]);
            Assert.AreEqual(expires, dict["expires"]);
        }


        [TestMethod]
        public async Task Login_Invalid_Credentials_Returns_Unauthorized()
        {
            // Arrange
            var username = "testuser";
            var password = "testpassword";
            var model = new LoginModel { Username = username, Password = password };

            _mediatorMock.Setup(x => x.Send(It.IsAny<FindUserByNameQuery>(), default)).ReturnsAsync((IdentityUser)null);

            // Act
            var result = await _controller.Login(model);

            // Assert
            var unauthorizedResult = result as UnauthorizedResult;
            Assert.IsNotNull(unauthorizedResult);
        }
    }
}
