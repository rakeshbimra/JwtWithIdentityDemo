using JwtWithIdentityDemo.Application.Abstractions.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Users.Queries.Handlers
{
    public class CheckRoleExistsQueryHandler : IRequestHandler<CheckRoleExistsQuery, bool>
    {
        private readonly IUserRoleManagerWrapper _userRoleManager;

        public CheckRoleExistsQueryHandler(IUserRoleManagerWrapper userRoleManager)
        {
            _userRoleManager = userRoleManager;
        }

        public async Task<bool> Handle(CheckRoleExistsQuery request, CancellationToken cancellationToken)
        {
            return await _userRoleManager.RoleExistsAsync(request.UserRole);
        }
    }
}
