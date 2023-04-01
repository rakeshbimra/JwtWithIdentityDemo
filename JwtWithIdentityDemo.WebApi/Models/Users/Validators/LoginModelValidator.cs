using FluentValidation;

namespace JwtWithIdentityDemo.WebApi.Models.Users.Validators
{
    public class LoginModelValidator:AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Username).NotEmpty();

            RuleFor(x => x.Password).NotEmpty();    
        }
    }
}
