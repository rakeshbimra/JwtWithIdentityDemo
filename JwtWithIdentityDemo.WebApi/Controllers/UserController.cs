using JwtWithIdentityDemo.Application.Users.Commands;
using JwtWithIdentityDemo.Application.Users.Queries;
using JwtWithIdentityDemo.WebApi.Models.Users;
using JwtWithIdentityDemo.WebApi.Models.Users.Validators;
using JwtWithIdentityDemo.WebApi.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            try
            {
                var registerUserModelValidator = new RegisterUserModelValidator();

                var validationResult = await registerUserModelValidator.ValidateAsync(model);

                if (!validationResult.IsValid)
                {
                    return new BadRequestResponse
                    {
                        Errors = validationResult.Errors
                                .GroupBy(e => e.PropertyName)
                                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToList())
                    }; ;
                }

                var userExists = await _mediator.Send(new FindUserByNameQuery(model.Username));

                if (userExists != null)

                    return new BadRequestResponse
                    {
                        Errors = new Dictionary<string, List<string>>
                    {
                        { "UserAlreadyExists!", new List<string> { "User already exists in the system." } }
                    }
                    };

                IdentityUser user = new()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username
                };

                var result = await _mediator.Send(new CreateUserCommand(user,model.Password));

                if (!result.Succeeded)

                    return new ErrorResponse
                    {
                        Message = "Error while creating user",
                        Errors = result.Errors.ToDictionary(e => e.Code, e => new List<string> { e.Description })
                    };

                _logger.LogInformation("User {UserName} created successfully!", model.Username);

                return new SuccessResponse
                {
                    Message = "User created successfully!"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating user: {Message}", ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
