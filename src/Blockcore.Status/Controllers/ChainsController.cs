using BlockcoreStatus.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using System.Threading.Tasks;


namespace BlockcoreStatus.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ChainsController : Controller
{
    private readonly IBlockcoreChainsService _chain;

    public ChainsController(IBlockcoreChainsService chain)
    {
        _chain = chain ?? throw new ArgumentNullException(nameof(chain));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _chain.GetAllChains();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}
