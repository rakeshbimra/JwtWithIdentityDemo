using JwtWithIdentityDemo.Application.Users.Commands;
using JwtWithIdentityDemo.Application.Users.Queries;
using JwtWithIdentityDemo.Application.WeatherForecasts.Queries;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.IoC
{
    public static class RegisterApplication
    {
        public static void AddApplication(this IServiceCollection services,IConfiguration configuration)
        {
            
            services.AddScoped<IRequestHandler<CreateUserCommand, IdentityResult>, CreateUserCommandHandler>();
            services.AddScoped<IRequestHandler<CheckPasswordQuery, bool>, CheckPasswordQueryHandler>();
            services.AddScoped<IRequestHandler<FindUserByNameQuery, IdentityUser>, FindUserByNameQueryHandler>();
            services.AddScoped<IRequestHandler<GetRolesQuery, IList<string>>, GetRolesQueryHandler>();
            services.AddScoped<IRequestHandler<GetWeatherForecastsQuery,IEnumerable<WeatherForecast>>, GetWeatherForecastsQueryHandler>();

        }
    }
}
