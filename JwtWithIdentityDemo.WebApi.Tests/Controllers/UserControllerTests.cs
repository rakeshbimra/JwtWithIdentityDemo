using FluentValidation.Results;
using JwtWithIdentityDemo.Application.Users.Commands;
using JwtWithIdentityDemo.Application.Users.Queries;
using JwtWithIdentityDemo.WebApi.Controllers;
using JwtWithIdentityDemo.WebApi.Models.Users;
using JwtWithIdentityDemo.WebApi.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
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
    public class UserControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private ILogger<UserController> _loggerMock;
        private UserController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = Mock.Of<ILogger<UserController>>();
            _controller = new UserController( _loggerMock, _mediatorMock.Object);
        }

        [TestMethod]
        public async Task Register_ValidModel_Returns_Ok()
        {
            // Arrange
            var model = new RegisterUserModel
            {
                Email = "test@test.com",
                Username = "testuser",
                Password = "P@ssword123"
            };
            var validationResult = new ValidationResult();
            _mediatorMock.Setup(x => x.Send(It.IsAny<FindUserByNameQuery>(), default)).ReturnsAsync((IdentityUser)null);
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), default)).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Register(model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(SuccessResponse));
        }

        [TestMethod]
        public async Task Register_InvalidModel_Returns_BadRequest()
        {
            // Arrange
            var model = new RegisterUserModel
            {
                Email = "test",
                Username = "",
                Password = "password"
            };
            var validationResult = new ValidationResult(new[] { new ValidationFailure("Email", "Invalid email"), new ValidationFailure("Username", "Username is required") });
            _mediatorMock.Setup(x => x.Send(It.IsAny<FindUserByNameQuery>(), default)).ReturnsAsync((IdentityUser)null);

            // Act
            var result = await _controller.Register(model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResponse));
        }

        [TestMethod]
        public async Task Register_CreateUserFailed_Returns_BadRequest()
        {
            // Arrange
            var model = new RegisterUserModel
            {
                Email = "test@test.com",
                Username = "testuser",
                Password = "P@ssword123"
            };

            var validationResult = new ValidationResult();
            var error = new IdentityError { Code = "Error", Description = "Error creating user" };
            _mediatorMock.Setup(x => x.Send(It.IsAny<FindUserByNameQuery>(), default)).ReturnsAsync((IdentityUser)null);
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), default)).ReturnsAsync(IdentityResult.Failed(error));

            // Act
            var result = await _controller.Register(model);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestResponse));
        }
    }

}
