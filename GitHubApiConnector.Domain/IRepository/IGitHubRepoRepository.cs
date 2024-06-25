using GitHubApiConnector.Domain.Entities;
using GitHubApiConnector.Domain.SeedWork;

namespace GitHubApiConnector.Domain.IRepository;

public interface IGitHubRepoRepository : IRepository<GitHubRepo>
{
    Task SaveAsync(GitHubRepo gitHubRepo);
    Task<List<GitHubRepo>> GetAllAsync();
    Task<GitHubRepo> GetByIdAsync(Guid id);
}