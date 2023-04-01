using JwtWithIdentityDemo.Application.Users.Commands.Handlers;
using JwtWithIdentityDemo.Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JwtWithIdentityDemo.Application.Users.Queries.Handlers;
using JwtWithIdentityDemo.Application.Users.Queries;

namespace JwtWithIdentityDemo.Application.Users.IoC
{
    public static class RegisterUsers
    {
        public static void AddUsers(this IServiceCollection services)
        {
            AddUserCommands(services);
            AddUserQueries(services);
        }

        private static void AddUserCommands(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<CreateUserCommand, IdentityResult>, CreateUserCommandHandler>();
            services.AddTransient<IRequestHandler<CreateRoleCommand, IdentityResult>, CreateRoleCommandHandler>();
            services.AddTransient<IRequestHandler<AddToRoleCommand, IdentityResult>, AddToRoleCommandHandler>();
        }

        private static void AddUserQueries(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandler<CheckPasswordQuery, bool>, CheckPasswordQueryHandler>();
            services.AddTransient<IRequestHandler<FindUserByNameQuery, IdentityUser>, FindUserByNameQueryHandler>();
            services.AddTransient<IRequestHandler<GetRolesQuery, IList<string>>, GetRolesQueryHandler>();
            services.AddTransient<IRequestHandler<CheckRoleExistsQuery, bool>, CheckRoleExistsQueryHandler>();
        }
    }
}
