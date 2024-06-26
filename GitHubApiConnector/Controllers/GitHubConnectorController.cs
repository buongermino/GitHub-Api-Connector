using GitHubApiConnector.UseCases;
using GitHubApiConnector.UseCases.DTOs;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GitHubApiConnector.Api.Controllers;

[Produces("application/json")]
[Route("api/repositories")]
[ApiController]
public class GitHubConnectorController : ControllerBase
{
    [HttpGet("fetch-and-save")]
    [SwaggerOperation(Summary = "Fetch github API and stores featured repositories", Description = "Fetch github API and stores featured repositories of some chosen languages")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Returns no content")]
    public async Task<IActionResult> FetchAndSaveRepositories([FromServices] IFetchAndSaveRepositoriesUseCase fetchAndSaveRepositoriesUseCase)
    {
        await fetchAndSaveRepositoriesUseCase.Execute();

        return NoContent();
    }

    [HttpGet("all")]
    [SwaggerOperation(Summary = "Returns a list of repositories", 
                        Description = "Returns a list of repositories, per pagination (deafult value is 10) and can be used a language as filter. If the specified language does not exists, no repos will be listed")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns a list of repositories, per pagination (deafult value is 10)", typeof(List<GitHubRepositoryDTO>))]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Returns no content if there is no repository in database")]
    public async Task<IActionResult> ListRepositories([FromServices] IGetAllGitHubRepositoriesUseCase getAllGitHubRepositoriesUseCase,
                                                        string filterByLanguage = "", int pageNumber = 1, int pageSize = 10)
    {
        var repositories = await getAllGitHubRepositoriesUseCase.Execute(filterByLanguage, pageNumber, pageSize);

        if (repositories.Count == 0) 
            return NoContent();

        return Ok(repositories);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get a repository details by ID", Description = "Returns the requested repository details by Id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns the request repository", typeof(GitHubRepositoryDetailsDTO))]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Returns no content if the repository could not be found")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "If the Id is invalid")]
    public async Task<IActionResult> ListRepositoryById([FromServices] IGetGitHubRepositoryByIdUseCase getGitHubRepositoryByIdUseCase, Guid id)
    {
        if (id == Guid.Empty) return BadRequest();

        var repository = await getGitHubRepositoryByIdUseCase.Execute(id);

        if (repository == null)
            return NoContent();

        return Ok(repository);
    }
}