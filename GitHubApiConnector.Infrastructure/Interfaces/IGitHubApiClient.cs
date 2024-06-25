namespace GitHubApiConnector.Infrastructure.Interfaces;

public interface IGitHubApiClient
{
    Task<List<GitHubRepositoryResponse>> FetchRepositories(List<string> languages);
}