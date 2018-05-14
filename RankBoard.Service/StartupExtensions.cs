using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RankBoard.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Service
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RankBoardDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("RankBoardDb")));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("RankBoardUsersDb")));

            return services;
        }
    }
}
