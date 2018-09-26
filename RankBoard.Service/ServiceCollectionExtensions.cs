using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RankBoard.Data.Contexts;
using RankBoard.Repositories;
using RankBoard.Repositories.Implementation.UnitOfWork;
using RankBoard.Repositories.Interface.UnitOfWork;

namespace RankBoard.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRankBoardDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RankBoardDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("RankBoardDb")));

            services.AddAutoMapper();

            services.AddScoped<IUnitOfWork, UnitOfWork>(provider =>
                new UnitOfWork(
                    new RankBoardDbContext(
                        new DbContextOptionsBuilder<RankBoardDbContext>()
                        .UseSqlServer(configuration.GetConnectionString("RankBoardDb"))
                        .Options)));

            return services;
        }

        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("RankBoardUsersDb")));

            services.AddAutoMapper();

            services.AddScoped<IUnitOfWorkIdentity, UnitOfWorkIdentity>(provider => 
                new UnitOfWorkIdentity(
                    new ApplicationDbContext(
                        new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseSqlServer(configuration.GetConnectionString("RankBoardUsersDb"))
                        .Options)));

            /****
             
             NOTE: Using AddScoped for the IUnitOfWork allows a single instance 
             of the underlying IDbCconnection to be used per request, as opposed 
             to AddTransient which would create a new instance each time it is resolved. 
             This is what allows the UnitOfWork pattern to work with multiple repositories
             
             ****/

            return services;
        }
    }
}
