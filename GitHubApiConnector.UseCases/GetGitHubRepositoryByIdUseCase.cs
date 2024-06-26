using AutoMapper;
using GitHubApiConnector.Domain.Entities;
using GitHubApiConnector.Domain.IRepository;
using GitHubApiConnector.UseCases.DTOs;

namespace GitHubApiConnector.UseCases;

public interface IGetGitHubRepositoryByIdUseCase
{
    Task<GitHubRepositoryDetailsDTO> Execute(Guid id);
}

public class GetGitHubRepositoryByIdUseCase(IGitHubRepoRepository repository, IMapper mapper) : IGetGitHubRepositoryByIdUseCase
{
    public async Task<GitHubRepositoryDetailsDTO> Execute(Guid id)
    {
        var repo = await repository.GetByIdAsync(id);

        var repositoryDetailsResponse = mapper.Map<GitHubRepositoryDetailsDTO>(repo);

        return repositoryDetailsResponse;
    }
}
