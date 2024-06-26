using GitHubApiConnector.Domain.Entities;
using GitHubApiConnector.Domain.IRepository;

namespace GitHubApiConnector.UseCases;

public interface IGetGitHubRepositoryByIdUseCase
{
    Task<GitHubRepo> Execute(Guid id);
}

public class GetGitHubRepositoryByIdUseCase(IGitHubRepoRepository repository) : IGetGitHubRepositoryByIdUseCase
{
    public async Task<GitHubRepo> Execute(Guid id)
    {
        var repo = await repository.GetByIdAsync(id);

        return repo;
    }
}
