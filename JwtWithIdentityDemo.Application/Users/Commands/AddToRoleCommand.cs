using JwtWithIdentityDemo.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Users.Commands
{
    public record AddToRoleCommand(IdentityUser IdentityUser, string UserRole) : IRequest<IdentityResult>;

}
