using JwtWithIdentityDemo.Application.Users.IoC;
using JwtWithIdentityDemo.Application.WeatherForecasts.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JwtWithIdentityDemo.Application.IoC
{
    public static class RegisterApplication
    {
        public static void AddApplication(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddUsers();
            services.AddWeatherForcast();
        }
    }
}
