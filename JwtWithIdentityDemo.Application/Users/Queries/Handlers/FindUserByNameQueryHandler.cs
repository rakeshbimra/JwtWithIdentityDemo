using JwtWithIdentityDemo.Application.Abstractions.Authentication;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Users.Queries.Handlers
{
    public class FindUserByNameQueryHandler : IRequestHandler<FindUserByNameQuery, IdentityUser>
    {
        private readonly IUserManagerWrapper _userManager;

        public FindUserByNameQueryHandler(IUserManagerWrapper userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityUser> Handle(FindUserByNameQuery request, CancellationToken cancellationToken)
        {
            var result = await _userManager.FindByNameAsync(request.username);

            return result;
        }
    }
}
