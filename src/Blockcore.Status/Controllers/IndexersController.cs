using BlockcoreStatus.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using System.Threading.Tasks;


namespace BlockcoreStatus.Controllers;
[Route("api/[controller]")]
[ApiController]
public class IndexersController : Controller
{
    private readonly IBlockcoreIndexersService _indexer;

    public IndexersController(IBlockcoreIndexersService indexer)
    {
        _indexer = indexer ?? throw new ArgumentNullException(nameof(indexer));
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _indexer.GetAllIndexer();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
}
