using JwtWithIdentityDemo.Application.Abstractions.Authentication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Users.Queries
{
    public class CheckPasswordQueryHandler : IRequestHandler<CheckPasswordQuery, bool>
    {
        private readonly IUserManagerWrapper _userManager;

        public CheckPasswordQueryHandler(IUserManagerWrapper userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(CheckPasswordQuery request, CancellationToken cancellationToken)
        {
            var result = await _userManager.CheckPasswordAsync(request.User, request.Password);

            return result;
        }
    }
}
