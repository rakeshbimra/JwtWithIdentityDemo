using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Commands.Users
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.RegisterUser.Email).NotEmpty();
            RuleFor(x => x.RegisterUser.Password).NotEmpty();
            RuleFor(x => x.RegisterUser.UserName).NotEmpty();
        }
    }
}
