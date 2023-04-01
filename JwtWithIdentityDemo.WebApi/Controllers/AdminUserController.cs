using JwtWithIdentityDemo.Application.Users.Commands;
using JwtWithIdentityDemo.Application.Users.Queries;
using JwtWithIdentityDemo.Common.Constants;
using JwtWithIdentityDemo.WebApi.Models.Users;
using JwtWithIdentityDemo.WebApi.Models.Users.Validators;
using JwtWithIdentityDemo.WebApi.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JwtWithIdentityDemo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : Controller
    {
        private readonly ILogger<AdminUserController> _logger;
        private readonly IMediator _mediator;

        public AdminUserController(ILogger<AdminUserController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterAdminUserModel model)
        {
            var validator = new RegisterAdminUserModelValidator();

            var validationResult = await validator.ValidateAsync(model);

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

            var result = await _mediator.Send(new CreateUserCommand(user, model.Password));

            if (!result.Succeeded)

                return new ErrorResponse
                {
                    Message = "Error while creating user",
                    Errors = result.Errors.ToDictionary(e => e.Code, e => new List<string> { e.Description })
                };

            _logger.LogInformation("Admin User {UserName} created successfully!", model.Username);

            if (!await _mediator.Send(new CheckRoleExistsQuery(UserRoles.Admin)))
            {
                await _mediator.Send(new CreateRoleCommand(new IdentityRole(UserRoles.Admin)));
            }

            var roleResult = await _mediator.Send(new AddToRoleCommand(user, UserRoles.Admin));

            return new SuccessResponse
            {
                Message = "User created successfully!"
            };
        }
    }
}
