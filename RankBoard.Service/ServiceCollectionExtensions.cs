using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RankBoard.Data.Contexts;
using RankBoard.Repositories;

namespace RankBoard.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRankBoardDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RankBoardDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("RankBoardDb")));

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

            services.AddScoped<IUnitOfWork, UnitOfWorkIdentity>(provider => 
                new UnitOfWorkIdentity(
                    new ApplicationDbContext(
                        new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseSqlServer(configuration.GetConnectionString("RankBoardUsersDb"))
                        .Options)));
            
            return services;
        }
    }
}
