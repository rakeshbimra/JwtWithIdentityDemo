using JwtWithIdentityDemo.Application.WeatherForecasts.Queries;
using JwtWithIdentityDemo.Common.Constants;
using JwtWithIdentityDemo.WebApi.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtWithIdentityDemo.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeatherForecastController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new UnauthorizedResponse();
            }

            var result = await _mediator.Send(new GetWeatherForecastsQuery());

            return new SuccessResponse<IEnumerable<WeatherForecast>>()
            {
                Data = result,
            };
        }
    }
}
