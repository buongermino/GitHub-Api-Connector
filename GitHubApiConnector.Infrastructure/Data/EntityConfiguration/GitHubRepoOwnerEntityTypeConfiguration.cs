using GitHubApiConnector.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GitHubApiConnector.Infrastructure.Data.EntityConfiguration;

public class GitHubRepoOwnerEntityTypeConfiguration : IEntityTypeConfiguration<GitHubRepoOwner>
{
    public void Configure(EntityTypeBuilder<GitHubRepoOwner> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.GitHubUserId);

        builder.Property(p => p.Login);

        builder.Property(p => p.Url); 
        
        builder.Property(p => p.AvatarUrl);

        builder.Property(p => p.ReposUrl);

        builder.Property(p => p.HtmlUrl);

        builder.Property(p => p.Type);

        builder.Property(p => p.OrganizationsUrl);

        builder.HasMany(p => p.Repos)
            .WithOne(p => p.Owner)
            .HasForeignKey(p => p.GitHubRepoOwnerId);
    }
}
