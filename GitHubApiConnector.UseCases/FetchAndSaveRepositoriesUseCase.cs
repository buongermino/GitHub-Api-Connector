using GitHubApiConnector.Domain.Entities;
using GitHubApiConnector.Domain.IRepository;
using GitHubApiConnector.Infrastructure;
using GitHubApiConnector.Infrastructure.Interfaces;

namespace GitHubApiConnector.UseCases;

public interface IFetchAndSaveRepositoriesUseCase
{
    Task Execute();
}

public class FetchAndSaveRepositoriesUseCase(IGitHubApiClient gitHubApiClient, IGitHubRepoRepository repository) : IFetchAndSaveRepositoriesUseCase
{
    public async Task Execute()
    {
        var languages = new List<string>() { "csharp", "java", "angular", "golang", "kotlin" };

        var repos = await gitHubApiClient.FetchRepositories(languages);

        var reposToSave = new List<GitHubRepo>();

        foreach (var repo in repos.Items) 
        {
            var entity = MapResponseToEntity(repo);
            reposToSave.Add(entity);
        }

        await SaveRepositoryIfNotAlreadyExists(reposToSave);
    }

    private GitHubRepo MapResponseToEntity(ItemResponse response)
    {
        GitHubRepo entity = new ()
        {
            Name = response.Name,
            Description = response.Description,
            Language = response.Language,
            Forks = response.Forks,
            Watchers = response.Watchers,
            FullName = response.FullName,
            GitHubId = response.Id.ToString(),
            CloneUrl = response.CloneUrl ?? "",
            HtmlUrl = response.HtmlUrl ?? "",
            GitUrl = response.GitUrl ?? "",
            SshUrl = response.SshUrl ?? "",
            Owner = new GitHubRepoOwner() {
                 Login = response.Owner.Login,
                 GitHubUserId = response.Owner.Id.ToString(),
                 AvatarUrl = response.Owner.AvatarUrl,
                 HtmlUrl = response.Owner.HtmlUrl,
                 OrganizationsUrl = response.Owner.OrganizationsUrl,
                 ReposUrl = response.Owner.ReposUrl,
                 Type = response.Owner.Type,
                 Url = response.Owner.Url,
            },
            Size = response.Size,
            Url = response.Url,
            CreatedAt = Convert.ToDateTime(response.CreatedAt),
            UpdatedAt = Convert.ToDateTime(response.UpdatedAt),
            PushedAt = Convert.ToDateTime(response.PushedAt)
        };
        entity.GitHubRepoOwnerId = entity.Owner.Id;

        return entity;
    }

    private async Task SaveRepositoryIfNotAlreadyExists(List<GitHubRepo> reposToSave)
    {
        var reposAlreadySaved = await repository.GetAllAsync();
        
        foreach (var repo in reposToSave) 
        {
            var repoAlreadySaved = reposAlreadySaved.FirstOrDefault(r => r.GitHubId == repo.GitHubId);

            if (repoAlreadySaved == null)
                await repository.SaveAsync(repo);
        }

        await repository.UnitOfWork.SaveEntitiesAsync();
    }
}