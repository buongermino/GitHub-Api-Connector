using GitHubApiConnector.Infrastructure;
using GitHubApiConnector.Infrastructure.Interfaces;

namespace GitHubApiConnector.UseCases;

public interface IFetchAndSaveRepositoriesUseCase
{
    Task<GitHubRepositoryResponse> Execute();
}

public class FetchAndSaveRepositoriesUseCase(IGitHubApiClient gitHubApiClient) : IFetchAndSaveRepositoriesUseCase
{
    public async Task<GitHubRepositoryResponse> Execute()
    {
        var languages = new List<string>() { "csharp", "java", "angular", "golang", "kotlin" };

        var repos = await gitHubApiClient.FetchRepositories(languages);

        return repos;
    }
}