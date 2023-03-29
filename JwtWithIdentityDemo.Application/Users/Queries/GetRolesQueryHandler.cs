using JwtWithIdentityDemo.Application.Abstractions.Authentication;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Users.Queries
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IList<string>>
    {
        private readonly IUserManagerWrapper _userManager;
        private readonly ILogger<GetRolesQueryHandler> _logger;

        public GetRolesQueryHandler(IUserManagerWrapper userManager, ILogger<GetRolesQueryHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IList<string>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting roles for user {UserId}", request.IdentityUser.Id);
                var result = await _userManager.GetRolesAsync(request.IdentityUser);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting roles for user {UserId}", request.IdentityUser.Id);
                throw;
            }
        }
    }
}
