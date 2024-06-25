using GitHubApiConnector.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace GitHubApiConnector.Api.Controllers;

[Route("api/[controller]/repositories")]
[ApiController]
public class GitHubConnectorController : ControllerBase
{
    [HttpGet("fetch-and-save")]
    public async Task<IActionResult> FetchAndSaveRepositories([FromServices] IFetchAndSaveRepositoriesUseCase fetchAndSaveRepositoriesUseCase)
    {
        await fetchAndSaveRepositoriesUseCase.Execute();

        return Ok();
    }

    [HttpGet("all")]
    public async Task<IActionResult> ListRepositories([FromServices] IGetAllGitHubRepositoriesUseCase getAllGitHubRepositoriesUseCase,
                                                        string filterByLanguage = "", int pageNumber = 1, int pageSize = 10)
    {
        var repositories = await getAllGitHubRepositoriesUseCase.Execute(filterByLanguage, pageNumber, pageSize);

        return Ok(repositories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ListRepositoryById()
    {
      return Ok();
    }
}