using GitHubApiConnector.Domain.Entities;
using GitHubApiConnector.Domain.SeedWork;
using GitHubApiConnector.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace GitHubApiConnector.Infrastructure.Data;

public class GitHubRepoContext : DbContext, IUnitOfWork
{
    public GitHubRepoContext()
    {
    }

    public GitHubRepoContext(DbContextOptions<GitHubRepoContext> options) : base(options) { }

    public DbSet<GitHubRepo> GitHubRepos { get; set; }
    public DbSet<GitHubRepoOwner> GitHubRepoOwners { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GitHubRepoEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GitHubRepoOwnerEntityTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await base.SaveChangesAsync(cancellationToken);

        return true;
    }
}

public class BankingContextDesignFactory : IDesignTimeDbContextFactory<GitHubRepoContext>
{
    public GitHubRepoContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../GitHubApiConnector.Api");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<GitHubRepoContext>()
            .UseSqlServer(connectionString);

        return new GitHubRepoContext(optionsBuilder.Options);
    }
}