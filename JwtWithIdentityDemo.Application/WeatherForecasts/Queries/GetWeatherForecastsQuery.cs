using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.WeatherForecasts.Queries
{
    public record GetWeatherForecastsQuery : IRequest<IEnumerable<WeatherForecast>>;
}
