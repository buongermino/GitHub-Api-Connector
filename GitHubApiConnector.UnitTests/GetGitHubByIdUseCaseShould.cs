using AutoMapper;
using FluentAssertions;
using GitHubApiConnector.Domain.Entities;
using GitHubApiConnector.Infrastructure.Data;
using GitHubApiConnector.Infrastructure.Data.Repositories;
using GitHubApiConnector.UseCases;
using Microsoft.EntityFrameworkCore;

namespace GitHubApiConnector.UnitTests;

public class GetGitHubByIdUseCaseShould
{
    private readonly IMapper _mapper;

    public GetGitHubByIdUseCaseShould()
    {
        _mapper = MapEntitiesHelper.CreateMapper();
    }
    private DbContextOptions<GitHubRepoContext> CreateInMemoryOptions()
    {
        return new DbContextOptionsBuilder<GitHubRepoContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    private GitHubRepoOwner CreateTestOwner()
    {
        return new GitHubRepoOwner
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
    }

    private GitHubRepo CreateTestRepo(GitHubRepoOwner owner)
    {
        return new GitHubRepo
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
            GitHubRepoOwnerId = owner.Id,
            Size = 2000,
            Owner = owner
        };
    }

    private async Task SeedDatabase(GitHubRepoContext context, GitHubRepoOwner owner, GitHubRepo repo)
    {
        context.GitHubRepoOwners.Add(owner);
        context.GitHubRepos.Add(repo);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task ShouldReturnsRepoById()
    {
        var options = CreateInMemoryOptions();

        var owner = CreateTestOwner();
        var repo = CreateTestRepo(owner);

        using (var context = new GitHubRepoContext(options))
        {
            await SeedDatabase(context, owner, repo);
        }

        using (var context = new GitHubRepoContext(options))
        {
            var repository = new GitHubRepoRepository(context);
            var useCase = new GetGitHubRepositoryByIdUseCase(repository, _mapper);
            var result = await useCase.Execute(repo.Id);

            result.Should().NotBeNull();
            result.Id.Should().Be(repo.Id);
            result.FullName.Should().Be(repo.FullName);
            result.Description.Should().Be(repo.Description);
            result.GitUrl.Should().Be(repo.GitUrl);
            result.Watchers.Should().Be(repo.Watchers);
            result.Owner.Should().NotBeNull();
            result.Owner.Id.Should().Be(owner.Id);
        }
    }

    [Fact]
    public async Task ShouldReturnsNull()
    {
        var options = CreateInMemoryOptions();
        var nonexistentId = Guid.NewGuid();

        using var context = new GitHubRepoContext(options);
        var repository = new GitHubRepoRepository(context);
        var useCase = new GetGitHubRepositoryByIdUseCase(repository, _mapper);
        var result = await useCase.Execute(nonexistentId);

        result.Should().BeNull();
    }
}
