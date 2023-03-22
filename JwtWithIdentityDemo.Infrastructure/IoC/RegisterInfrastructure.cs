using JwtWithIdentityDemo.Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Infrastructure.IoC
{
    public static class RegisterInfrastructure
    {
        public static void AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JwtSetings>(configuration.GetSection(JwtSetings.SectionName));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        }

    }
}
