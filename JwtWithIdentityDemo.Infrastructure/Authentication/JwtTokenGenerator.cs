using JwtWithIdentityDemo.Application.Abstractions.Authentication;
using JwtWithIdentityDemo.Infrastructure.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSetingsOption;

        public JwtTokenGenerator(IOptions<JwtSettings> jwtSetings)
        {

            _jwtSetingsOption = jwtSetings.Value;

        }
        public (string token, DateTime expires) GenerateToken(Guid userId, string userName, IList<string> userRoles)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSetingsOption.Secret)),
                    SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,userId.ToString()),
                new Claim(ClaimTypes.Name,userName),
                new Claim (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }


            var securityToken = new JwtSecurityToken(
                issuer: _jwtSetingsOption.Issuer,
                audience: _jwtSetingsOption.Audience,
                expires: DateTime.Now.AddMinutes(_jwtSetingsOption.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredentials);

            return (new JwtSecurityTokenHandler().WriteToken(securityToken), securityToken.ValidTo);
        }
    }
}
