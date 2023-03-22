using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Infrastructure.Authentication
{
    public class JwtSetings
    {
        public const string SectionName = "JwtSetings";
        public string Secret { get; init; } = null;
        public string Issuer { get; init; } = null;
        public string Audience { get; init; } = null;
        public int ExpiryMinutes { get; init; } 
    }
}
