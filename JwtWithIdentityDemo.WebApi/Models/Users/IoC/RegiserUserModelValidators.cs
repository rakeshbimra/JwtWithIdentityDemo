using FluentValidation;
using JwtWithIdentityDemo.WebApi.Models.Users.Validators;

namespace JwtWithIdentityDemo.WebApi.Models.Users.IoC
{
    public static class RegiserUserModelValidators
    {
        public static void AddUserModelValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<LoginModel>, LoginModelValidator>();

        }
    }
}
