using GitHubApiConnector.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GitHubApiConnector.Infrastructure.Data.EntityConfiguration;

public class GitHubRepoEntityTypeConfiguration : IEntityTypeConfiguration<GitHubRepo>
{
    public void Configure(EntityTypeBuilder<GitHubRepo> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.GitHubId);

        builder.Property(p => p.Name);

        builder.Property(p => p.FullName);

        builder.Property(p => p.Description);

        builder.Property(p => p.Url);

        builder.Property(p => p.GitUrl);

        builder.Property(p => p.SshUrl);

        builder.Property(p => p.HtmlUrl);

        builder.Property(p => p.CloneUrl);

        builder.Property(p => p.Forks);

        builder.Property(p => p.Language);

        builder.Property(p => p.CreatedAt);

        builder.Property(p => p.UpdatedAt);

        builder.Property(p => p.PushedAt);

        builder.Property(p => p.GitHubRepoOwnerId);

        builder.HasOne(p => p.Owner)
            .WithMany(p => p.Repos)
            .HasForeignKey(p => p.GitHubRepoOwnerId);
    }
}
