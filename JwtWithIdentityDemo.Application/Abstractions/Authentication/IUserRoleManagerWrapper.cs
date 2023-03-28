using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Abstractions.Authentication
{
    public interface IUserRoleManagerWrapper
    {
        Task<IdentityResult> CreateAsync(IdentityRole role);
        Task<IdentityResult> DeleteAsync(IdentityRole role);
        Task<IdentityRole> FindByIdAsync(string roleId);
        Task<IdentityRole> FindByNameAsync(string roleName);
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> UpdateAsync(IdentityRole role);
    }
}
