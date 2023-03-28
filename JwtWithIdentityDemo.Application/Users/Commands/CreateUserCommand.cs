using JwtWithIdentityDemo.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Users.Commands
{
    public record CreateUserCommand(RegisterUserDto RegisterUser) : IRequest<IdentityResult>;
}
