namespace GitHubApiConnector.UseCases.DTOs;

public class GitHubRepositoryDTO
{
    public Guid Id { get; set; }
    public string Language { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    public int Watchers { get; set; }
    public int Forks { get; set; }
    public string GitUrl { get; set; }
    public string CloneUrl { get; set; }
}
