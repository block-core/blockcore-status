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
        _github = github;
    }

    [HttpGet("[action]/{name}")]
    public async Task<IActionResult> GetOrganizationInfo(string name)
    {
        var orgname = await _github.GetOrganizationInfo(name);
        if (orgname == null)
        {
            return NotFound();
        }
        return Ok(orgname);
    }

}
