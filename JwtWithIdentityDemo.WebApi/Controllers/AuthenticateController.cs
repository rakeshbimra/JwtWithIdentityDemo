using FluentValidation;
using JwtWithIdentityDemo.Application.Abstractions.Authentication;
using JwtWithIdentityDemo.Application.Users.Queries;
using JwtWithIdentityDemo.WebApi.Models.Users;
using JwtWithIdentityDemo.WebApi.Models.Users.Validators;
using JwtWithIdentityDemo.WebApi.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;

namespace JwtWithIdentityDemo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ILogger<AuthenticateController> _logger;
        private readonly IMediator _mediator;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IValidator<LoginModel> _loginValidator;

        public AuthenticateController(ILogger<AuthenticateController> logger,
            IMediator mediator,
            IJwtTokenGenerator jwtTokenGenerator,
            IValidator<LoginModel> loginValidator)
        {
            _logger = logger;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mediator = mediator;
            _loginValidator = loginValidator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var validationResult = await _loginValidator.ValidateAsync(model);

                if (!validationResult.IsValid)
                {
                    return new BadRequestResponse
                    {
                        Errors = validationResult.Errors
                                .GroupBy(e => e.PropertyName)
                                .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToList())
                    }; ;
                }

                var user = await _mediator.Send(new FindUserByNameQuery(model.Username));

                if (user != null && await _mediator.Send(new CheckPasswordQuery(user, model.Password)))
                {
                    var userRoles = await _mediator.Send(new GetRolesQuery(user));

                    var (access_token, token_type, expires_in) = _jwtTokenGenerator.GenerateToken(Guid.Parse(user.Id), user.UserName, userRoles);

                    return Ok(new { access_token, token_type, expires_in });
                }

                else
                {
                    return new UnauthorizedResponse
                    {
                        Message = $"Authentication failed; invalid credentails"
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while attempting to log in user {Username}", model.Username);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
