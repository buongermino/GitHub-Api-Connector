using GitHubApiConnector.Infrastructure.Interfaces;
using System.Text.Json;

namespace GitHubApiConnector.Infrastructure;

public class GitHubApiClient(HttpClient httpClient) : IGitHubApiClient
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };


    public async Task<GitHubRepositoryResponse> FetchRepositories(List<string> languages)
    {
        SetHeaders(httpClient);
        var repositories = new GitHubRepositoryResponse();

        var response = await httpClient.GetAsync($"/search/repositories?q=language:{languages.First()}");

        if (response.IsSuccessStatusCode) 
        {
            var content = await response.Content.ReadAsStringAsync();

            repositories = JsonSerializer.Deserialize<GitHubRepositoryResponse>(content, _jsonSerializerOptions)!;
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
