using GitHubApiConnector.Domain.Entities;
using GitHubApiConnector.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace GitHubApiConnector.Api.Controllers;

[Route("api/[controller]/repositories")]
[ApiController]
public class GitHubConnectorController : ControllerBase
{
    [HttpGet("fetch-and-save")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> FetchAndSaveRepositories([FromServices] IFetchAndSaveRepositoriesUseCase fetchAndSaveRepositoriesUseCase)
    {
        await fetchAndSaveRepositoriesUseCase.Execute();

        return NoContent();
    }

    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GitHubRepo>))]
    public async Task<IActionResult> ListRepositories([FromServices] IGetAllGitHubRepositoriesUseCase getAllGitHubRepositoriesUseCase,
                                                        string filterByLanguage = "", int pageNumber = 1, int pageSize = 10)
    {
        var repositories = await getAllGitHubRepositoriesUseCase.Execute(filterByLanguage, pageNumber, pageSize);

        return Ok(repositories);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GitHubRepo))]
    public async Task<IActionResult> ListRepositoryById([FromServices] IGetGitHubRepositoryByIdUseCase getGitHubRepositoryByIdUseCase, Guid id)
    {
        if (id == Guid.Empty) return BadRequest();

        var repository = await getGitHubRepositoryByIdUseCase.Execute(id);

        if (repository == null)
            return NoContent();

        return Ok(repository);
    }
}