using JwtWithIdentityDemo.Application.Abstractions.Authentication;
using JwtWithIdentityDemo.Application.Users.Queries;
using JwtWithIdentityDemo.Application.Users.Queries.Handlers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Users.Commands.Handlers
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, IdentityResult>
    {
        private readonly ILogger<GetRolesQueryHandler> _logger;
        private readonly IUserRoleManagerWrapper _userRoleManager;

        public CreateRoleCommandHandler(ILogger<GetRolesQueryHandler> logger,
            IUserRoleManagerWrapper userRoleManager)
        {
            _logger = logger;
            _userRoleManager = userRoleManager;
        }

        public async Task<IdentityResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userRoleManager.CreateAsync(request.IdentityRole);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating roles {RoleName}", request.IdentityRole.Name);
                throw;
            }
        }
    }
}
