using GitHubApiConnector.Domain.SeedWork;

namespace GitHubApiConnector.Domain.Entities;

public class GitHubRepo : Entity
{
    public GitHubRepo() { }

    public string GitHubId { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public string GitUrl { get; set; }
    public string SshUrl { get; set; }
    public string CloneUrl { get; set; }
    public int Size { get; set; }
    public int Watchers { get; set; }
    public int Forks { get; set; }
    public string Language { get; set; }
    public string HtmlUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime PushedAt { get; set; }

    public Guid GitHubRepoOwnerId { get; set; }
    public GitHubRepoOwner Owner { get; set; }
}
