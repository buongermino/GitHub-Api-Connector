using AutoMapper;
using GitHubApiConnector.Domain.Entities;
using GitHubApiConnector.UseCases.DTOs;

namespace GitHubApiConnector.UseCases;

public class MapEntities : Profile
{
    public MapEntities()
    {
        CreateMap<GitHubRepo, GitHubRepositoryDTO>();
        CreateMap<GitHubRepo, GitHubRepositoryDetailsDTO>();
    }
}