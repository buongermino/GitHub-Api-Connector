using GitHubApiConnector.Infrastructure.Interfaces;
using System.Text.Json;

namespace GitHubApiConnector.Infrastructure;

public class GitHubApiClient(HttpClient httpClient) : IGitHubApiClient
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };


    public async Task<List<GitHubRepositoryResponse>> FetchRepositories(List<string> languages)
    {
        SetHeaders(httpClient);
        var repositoriesPerRequest = new GitHubRepositoryResponse();
        var repositories = new List<GitHubRepositoryResponse>();

        foreach (var language in languages)
        {
            var response = await httpClient.GetAsync($"/search/repositories?q=language:{language}&per_page=10&page=1");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                repositoriesPerRequest = JsonSerializer.Deserialize<GitHubRepositoryResponse>(content, _jsonSerializerOptions)!;
            }
            repositories.Add(repositoriesPerRequest);
        }

        return repositories;
    }

    private void SetHeaders(HttpClient httpClient)
    {
        httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
        httpClient.DefaultRequestHeaders.Add("User-Agent", "GitHubApiConnector");
        httpClient.BaseAddress = new Uri("https://api.github.com");
    }
}
