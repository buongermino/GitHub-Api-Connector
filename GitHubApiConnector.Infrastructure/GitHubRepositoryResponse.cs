using System.Text.Json.Serialization;

namespace GitHubApiConnector.Infrastructure;

public class GitHubRepositoryResponse
{
    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }
    public bool IncompleteResults { get; set; }
    public List<ItemResponse> Items { get; set; }
}

public class ItemResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public OwnerResponse Owner { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public int Size { get; set; }
    public int Watchers { get; set; }
    public int Forks { get; set; }
    public string Language { get; set; }

    [JsonPropertyName("git_url")]
    public string GitUrl { get; set; }
    [JsonPropertyName("ssh_url")]
    public string SshUrl { get; set; }
    [JsonPropertyName("clone_url")]
    public string CloneUrl { get; set; }

    [JsonPropertyName("full_name")]
    public string FullName { get; set; }

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; }

    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public string UpdatedAt { get; set; }

    [JsonPropertyName("pushed_at")]
    public string PushedAt { get; set; }
}

public class OwnerResponse
{
    public string Login { get; set; }
    public long Id { get; set; }
    public string Url { get; set; }
    public string Type { get; set; }

    [JsonPropertyName("avatar_url")]
    public string AvatarUrl { get; set; }

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; }

    [JsonPropertyName("repos_url")]
    public string ReposUrl { get; set; }

    [JsonPropertyName("organizations_url")]
    public string OrganizationsUrl { get; set; }
}