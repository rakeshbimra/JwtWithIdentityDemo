using JwtWithIdentityDemo.Application.Abstractions.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Queries.Users
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IList<string>>
    {
        private readonly IUserManagerWrapper _userManager;

        public GetRolesQueryHandler(IUserManagerWrapper userManager)
        {
            _userManager = userManager;
        }

        public Task<IList<string>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var result = _userManager.GetRolesAsync(request.IdentityUser);

            return result;
        }
    }
}
