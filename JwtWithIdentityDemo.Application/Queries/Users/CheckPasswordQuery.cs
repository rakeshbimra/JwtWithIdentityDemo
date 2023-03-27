using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtWithIdentityDemo.Application.Queries.Users
{
    public record CheckPasswordQuery(IdentityUser User, string Password) : IRequest<bool>;

}
