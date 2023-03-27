using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Abstractions.Authentication
{
    public interface IJwtTokenGenerator
    {
        (string token, DateTime expires) GenerateToken(Guid userId, string userName, IList<string> userRoles);
    }
}
