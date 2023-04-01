using FluentValidation;

namespace JwtWithIdentityDemo.WebApi.Models.Users.Validators
{
    public class RegisterAdminUserModelValidator:AbstractValidator<RegisterAdminUserModel>
    {
        public RegisterAdminUserModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty();

            RuleFor(x => x.Password).NotEmpty();

            RuleFor(x=>x.Username).NotEmpty();
        }
    }
}
