using System.ComponentModel.DataAnnotations;

namespace JwtWithIdentityDemo.WebApi.Models.Users
{
    public class LoginModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
