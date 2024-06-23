namespace GitHubApiConnector.Infrastructure.Interfaces;

public interface IGitHubApiClient
{
    Task<GitHubRepositoryResponse> FetchRepositories(List<string> languages);
}