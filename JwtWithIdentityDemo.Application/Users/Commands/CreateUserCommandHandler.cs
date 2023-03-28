using JwtWithIdentityDemo.Application.Abstractions.Authentication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IdentityResult>
    {
        private readonly IUserManagerWrapper _userManager;

        public CreateUserCommandHandler(IUserManagerWrapper userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new IdentityUser
            {
                UserName = request.RegisterUser.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = request.RegisterUser.Email
            };

            var result = await _userManager.CreateUserAsync(user, request.RegisterUser.Password);

            return result;
        }
    }
}
