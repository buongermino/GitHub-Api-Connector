using GitHubApiConnector.Domain.Entities;
using GitHubApiConnector.Domain.SeedWork;
using GitHubApiConnector.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GitHubApiConnector.Infrastructure.Data;

public class GitHubRepoContext : DbContext, IUnitOfWork
{
    public GitHubRepoContext() { }

    public GitHubRepoContext(DbContextOptions<GitHubRepoContext> options) : base(options) { }

    public DbSet<GitHubRepo> GitHubRepos { get; set; }
    public DbSet<GitHubRepoOwner> GitHubRepoOwners { get; set; }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../GitHubApiConnector"))
               .AddJsonFile("appsettings.json")
               .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GitHubRepoEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GitHubRepoOwnerEntityTypeConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
