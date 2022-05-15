using blockcore.status.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace blockcore.status.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GithubController : Controller
{
    private readonly IGithubService _github;

    public GithubController(IGithubService github)
    {
        _github = github ?? throw new ArgumentNullException(nameof(github));
    }

    [HttpGet("[action]/{name}")]
    public async Task<IActionResult> GetOrganizationInfo(string name)
    {
        var org = await _github.GetOrganizationInfo(name);
        if (org == null)
        {
            return NotFound();
        }
        return Ok(org);
    }

    [HttpGet("[action]/{owner}")]
    public async Task<IActionResult> GetAllPublicRepositories(string owner)
    {
        var repo = await _github.GetAllPublicRepositories(owner);
        if (repo == null)
        {
            return NotFound();
        }
        return Ok(repo);
    }


    [HttpGet("[action]/{owner}/{name}")]
    public async Task<IActionResult> GetRepositoryInfo(string owner, string name)
    {
        var repo = await _github.GetRepositoryInfo(owner, name);
        if (repo == null)
        {
            return NotFound();
        }
        return Ok(repo);
    }


    [HttpGet("[action]/{owner}/{name}")]
    public async Task<IActionResult> GetLatestRepositoryRelease(string owner, string name)
    {
        var releases = await _github.GetLatestRepositoryRelease(owner, name);
        if (releases == null)
        {
            return NotFound();
        }
        var lastTag = releases;
        return Ok(lastTag);
    }
}
