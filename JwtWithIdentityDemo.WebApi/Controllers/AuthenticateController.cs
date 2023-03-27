using JwtWithIdentityDemo.Application.Abstractions.Authentication;
using JwtWithIdentityDemo.Application.Queries.Users;
using JwtWithIdentityDemo.WebApi.Models.Users;
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
        private readonly IMediator _mediator;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthenticateController(IMediator mediator,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _mediator.Send(new FindUserByNameQuery(model.Username));

            if (user != null && await _mediator.Send(new CheckPasswordQuery(user, model.Password)))
            {
                var userRoles = await _mediator.Send(new GetRolesQuery(user));

                var (token, expires) = _jwtTokenGenerator.GenerateToken(Guid.Parse(user.Id), user.UserName, userRoles);

                return Ok(new { token, expires });
            }

            else
            {
                return Unauthorized();
            }
        }
    }
}
