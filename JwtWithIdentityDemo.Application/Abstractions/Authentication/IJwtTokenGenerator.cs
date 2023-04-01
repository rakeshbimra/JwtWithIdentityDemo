using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Abstractions.Authentication
{
    public interface IJwtTokenGenerator
    {
        (string access_token, string token_type, DateTime expires_in) GenerateToken(Guid userId, string userName, IList<string> userRoles);
    }
}
