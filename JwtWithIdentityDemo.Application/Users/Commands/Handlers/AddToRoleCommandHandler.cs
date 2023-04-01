using JwtWithIdentityDemo.Application.Abstractions.Authentication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Users.Commands.Handlers
{
    public class AddToRoleCommandHandler : IRequestHandler<AddToRoleCommand, IdentityResult>
    {
        private readonly IUserManagerWrapper _userManager;
        public AddToRoleCommandHandler(IUserManagerWrapper userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(AddToRoleCommand request, CancellationToken cancellationToken)
        {
            return await _userManager.AddToRolesAsync(request.IdentityUser, request.UserRole);
        }
    }
}
