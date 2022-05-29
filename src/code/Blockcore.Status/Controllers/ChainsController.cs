using blockcore.status.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using System.Threading.Tasks;


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
    public async Task<IActionResult> GetAllChains()
    {
        var result = await _chain.GetAllChains();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}
