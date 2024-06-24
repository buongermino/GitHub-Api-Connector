using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GitHubApiConnector.Infrastructure.Data;

namespace GitHubApiConnector.Infrastructure;

public static class InfrastructureModule
{
    public static void AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContexts(configuration);
    }

    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<GitHubRepoContext>(options =>
        {
            options.UseSqlServer(connectionString, ConfigureOptions);
        });

        return services;

        static void ConfigureOptions(SqlServerDbContextOptionsBuilder options)
        {
            options.MigrationsAssembly(typeof(InfrastructureModule).Assembly.FullName);
            options.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        }
    }
}
