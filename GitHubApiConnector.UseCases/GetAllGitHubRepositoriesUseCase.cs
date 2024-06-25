using GitHubApiConnector.Domain.Entities;
using GitHubApiConnector.Domain.IRepository;

namespace GitHubApiConnector.UseCases;

public interface IGetAllGitHubRepositoriesUseCase 
{
    Task<List<GitHubRepo>> Execute(string filterByLanguage, int pageNumber, int pageSize);
}


public class GetAllGitHubRepositoriesUseCase(IGitHubRepoRepository repository) : IGetAllGitHubRepositoriesUseCase
{
    public async Task<List<GitHubRepo>> Execute(string filterByLanguage, int pageNumber, int pageSize)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

        int skip = (pageNumber - 1) * pageSize;

        var repositories = await repository.GetAllAsync();

        if (!string.IsNullOrEmpty(filterByLanguage))
        {
            if (filterByLanguage == "csharp") filterByLanguage = "C#";
            repositories = repositories.Where(repo => repo.Language.Equals(filterByLanguage, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        List<GitHubRepo> paginatedRepos = repositories
                                           .Skip(skip)
                                           .Take(pageSize)
                                           .ToList();

        return paginatedRepos;
    }
}

