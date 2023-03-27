using FluentValidation;

namespace JwtWithIdentityDemo.WebApi.Models.Users.Validators
{
    public class RegisterUserModelValidator : AbstractValidator<RegisterUserModel>
    {
        public RegisterUserModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty();

            RuleFor(x => x.Password).NotEmpty();

            RuleFor(x => x.Username).NotEmpty();
        }
    }
}
