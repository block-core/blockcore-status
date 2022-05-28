using blockcore.status.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace blockcore.status.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ChainsController : Controller
{
    private readonly IBlockcoreChainsService _chain;

    public ChainsController(IBlockcoreChainsService chain)
    {
        _chain = chain ?? throw new ArgumentNullException(nameof(chain));
    }

    [HttpGet("[action]")]
    public IActionResult GetAllChains()
    {
        return Ok();
    }
}
