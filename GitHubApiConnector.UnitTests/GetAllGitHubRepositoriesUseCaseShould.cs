using AutoMapper;
using FluentAssertions;
using GitHubApiConnector.Infrastructure.Data;
using GitHubApiConnector.Infrastructure.Data.Repositories;
using GitHubApiConnector.UseCases;
using Microsoft.EntityFrameworkCore;

namespace GitHubApiConnector.UnitTests;

public class GetAllGitHubRepositoriesUseCaseShould
{
    private readonly IMapper _mapper;

    public GetAllGitHubRepositoriesUseCaseShould()
    {
        _mapper = MapEntitiesHelper.CreateMapper();
    }

    private DbContextOptions<GitHubRepoContext> CreateInMemoryOptions()
    {
        return new DbContextOptionsBuilder<GitHubRepoContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }

    private async Task SeedDatabase(GitHubRepoContext context)
    {
        SeedWork database = new();
        database.GitHubRepositoriesSeed(context);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task ShouldReturnsAllRepositories()
    {
        var options = CreateInMemoryOptions();

        using (var context = new GitHubRepoContext(options))
        {
            await SeedDatabase(context);
        }

        using (var context = new GitHubRepoContext(options))
        {
            var repository = new GitHubRepoRepository(context);
            var useCase = new GetAllGitHubRepositoriesUseCase(repository, _mapper);
            var result = await useCase.Execute("", 1, 10);

            result.Count.Should().BeGreaterThan(0);
            result.Should().NotBeNullOrEmpty();
        }
    }
}
