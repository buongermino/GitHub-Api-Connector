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
    public async Task<IActionResult> ListRepositories()
    {
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ListRepositoryById()
    {
      return Ok();
    }
}