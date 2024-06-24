using GitHubApiConnector.Domain.Entities;
using GitHubApiConnector.Domain.SeedWork;
using GitHubApiConnector.Infrastructure.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

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
            optionsBuilder.UseSqlServer("Data Source=NBQFC-1M38ST3;Database=github_repos_db;User ID=sa;Password=Fcamara@2024;Trust Server Certificate=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GitHubRepoEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GitHubRepoOwnerEntityTypeConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
