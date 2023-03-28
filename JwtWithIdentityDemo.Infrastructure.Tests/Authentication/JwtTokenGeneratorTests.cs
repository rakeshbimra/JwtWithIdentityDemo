using JwtWithIdentityDemo.Infrastructure.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Infrastructure.Tests.Authentication
{
    [TestClass]
    public class JwtTokenGeneratorTests
    {
        private  JwtSettings _jwtSettings;
        private JwtTokenGenerator _tokenGenerator;

        [TestInitialize]
        public void Initialize()
        {
            // Set up the JwtSettings options
            _jwtSettings = new JwtSettings
            {
                Secret = "super-secret-key",
                Issuer = "JwtTokenWithIdentity",
                Audience = "JwtTokenWithIdentity",
                ExpiryMinutes = 5
            };

            // Create an IOptions object with the JwtSettings options
            IOptions<JwtSettings> jwtSettingsOptions = Options.Create(_jwtSettings);

            // Create an instance of JwtTokenGenerator
            _tokenGenerator = new JwtTokenGenerator(jwtSettingsOptions);
        }
        
        [TestMethod]
        public void GenerateToken_Returns_Token_And_Expires()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string userName = "testuser@test.com";
            IList<string> userRoles = new List<string> {  };

            // Act
            (string token, DateTime expires) result = _tokenGenerator.GenerateToken(userId, userName, userRoles);

            // Assert
            Assert.IsNotNull(result.token);
            Assert.IsNotNull(result.expires);

            // Verify that the token can be decoded
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(result.token);
            Assert.AreEqual(_jwtSettings.Issuer, jwtToken.Issuer);
            Assert.AreEqual(_jwtSettings.Audience, jwtToken.Payload["aud"]);
            Assert.AreEqual(userName, jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value);
            Assert.AreEqual(userId.ToString(), jwtToken.Payload["sub"]);
            Assert.IsTrue(jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti) != null);
            foreach (var userRole in userRoles)
            {
                Assert.IsTrue(jwtToken.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == userRole));
            }
        }

    }
}
