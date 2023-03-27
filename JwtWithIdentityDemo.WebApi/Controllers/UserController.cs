using JwtWithIdentityDemo.Application.Commands.Users;
using JwtWithIdentityDemo.Application.Dtos;
using JwtWithIdentityDemo.Application.Queries.Users;
using JwtWithIdentityDemo.WebApi.Models.Users;
using JwtWithIdentityDemo.WebApi.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http.Description;

namespace JwtWithIdentityDemo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;
        

        public UserController(ILogger<UserController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
        {

            var userExists = await _mediator.Send(new FindUserByNameQuery(model.Username));

            if (userExists != null)

                return new BadRequestResponse
                {
                    Errors = new Dictionary<string, List<string>>
                    {
                        { "UserAlreadyExists!", new List<string> { "User already exists in the system." } }
                    }
                };

            var dto = new RegisterUserDto
            {
                Email = model.Email,
                UserName = model.Username,
                Password = model.Password,
            };

            var result =  await _mediator.Send(new CreateUserCommand(dto));

            if (!result.Succeeded)

                return new BadRequestResponse
                {
                    Message = "Error while creating user",
                    Errors = result.Errors.ToDictionary(e => e.Code, e => new List<string> { e.Description })
                };



            return new SuccessResponse
            {
                Message = "User created successfully!"
            };
        }
    }
}
