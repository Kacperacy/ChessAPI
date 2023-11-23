using Chess.Domain.Common.Services;
using Chess.Domain.Games.Repositories;
using Chess.Domain.Identity.Services;
using Chess.Infrastructure.Common.Services;
using Chess.Infrastructure.EF.Context;
using Chess.Infrastructure.Games.Services;
using Chess.Infrastructure.Identity.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Chess.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config =>
                config.RegisterServicesFromAssemblies(typeof(Extensions).Assembly));

            services.AddDbContext<EFContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DatabaseConnection")!,
                    opt =>
                    {
                        opt.CommandTimeout(30);
                        opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    }).LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
            });

            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IDateTimeService, DateTimeService>();
            services.AddScoped<IGameRepository, GameRepository>();
            
            return services;
        }
    }
}
