using JwtWithIdentityDemo.Common.Constants;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Users.Queries
{
    public record CheckRoleExistsQuery(string UserRole) : IRequest<bool>;
}
