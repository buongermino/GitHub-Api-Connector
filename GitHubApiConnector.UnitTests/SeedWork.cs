using GitHubApiConnector.Domain.Entities;
using GitHubApiConnector.Infrastructure.Data;
using GitHubApiConnector.UseCases;

namespace GitHubApiConnector.UnitTests;

public class SeedWork
{
    public SeedWork() { }

    public void GitHubRepositoriesSeed(GitHubRepoContext context)
    {
        var owner = new GitHubRepoOwner
        {
            Login = "TestOwner",
            GitHubUserId = "12345",
            Url = "https://api.github.com/users/TestOwner",
            AvatarUrl = "https://avatars.githubusercontent.com/u/12345?v=4",
            HtmlUrl = "https://github.com/TestOwner",
            ReposUrl = "https://api.github.com/users/TestOwner/repos",
            OrganizationsUrl = "https://api.github.com/users/TestOwner/orgs",
            Type = "TestUser"
        };

        var repositories = new List<GitHubRepo>()
        {
            new()
            {
                Name = "TestRepo",
                FullName = "Test/Repo",
                GitHubId = "12345",
                Description = "A test repository",
                Url = "https://api.github.com/users/TestOwner/repos",
                CloneUrl = "https://api.github.com/users/TestOwner/repos",
                GitUrl = "https://api.github.com/users/TestOwner/repos",
                HtmlUrl = "https://api.github.com/users/TestOwner/repos",
                SshUrl = "https://api.github.com/users/TestOwner/repos",
                Language = "C#",
                Forks = 500,
                Watchers = 2000,
                CreatedAt = DateTime.UtcNow.AddMonths(-2),
                PushedAt = DateTime.UtcNow.AddMonths(-1),
                UpdatedAt = DateTime.UtcNow.AddMonths(-1),
                Size = 2000,
                Owner = owner
            },
            new()
            {
                Name = "TestRepo2",
                FullName = "Test/Repo2",
                GitHubId = "12342225",
                Description = "A test repository 2",
                Url = "https://api.github.com/users/TestOwner/repos",
                CloneUrl = "https://api.github.com/users/TestOwner/repos",
                GitUrl = "https://api.github.com/users/TestOwner/repos",
                HtmlUrl = "https://api.github.com/users/TestOwner/repos",
                SshUrl = "https://api.github.com/users/TestOwner/repos",
                Language = "C#",
                Forks = 500,
                Watchers = 2000,
                CreatedAt = DateTime.UtcNow.AddMonths(-2),
                PushedAt = DateTime.UtcNow.AddMonths(-1),
                UpdatedAt = DateTime.UtcNow.AddMonths(-1),
                Size = 2000,
                Owner = owner
            }
        };

        context.GitHubRepoOwners.Add(owner);
        context.GitHubRepos.AddRange(repositories);
    }
}

