using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RankBoard.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RankBoard.Ids.Identity
{
    public static class IdentityBuilderExtensions
    {
        public static IdentityBuilder AddCustomStores(this IdentityBuilder builder)
        {
            builder.Services.AddTransient<IUserStore<ApplicationUserDto>, CustomUserStore>();
            builder.Services.AddTransient<IRoleStore<IdentityRole>, CustomRoleStore>();

            return builder;
        }
    }
}
