using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Abstractions.Authentication
{
    public interface IUserManagerWrapper
    {
        Task<IdentityResult> CreateUserAsync(IdentityUser user, string password);
        Task<IdentityUser> FindByNameAsync(string userName);
        Task<bool> CheckPasswordAsync(IdentityUser user, string password);
        Task<IList<string>> GetRolesAsync(IdentityUser user);
    }
}
