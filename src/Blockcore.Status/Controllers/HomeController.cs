using Blockcore.Status.Entities.Indexer;
using BlockcoreStatus.Services.Contracts;
using BlockcoreStatus.ViewModels.Home;
using BreadCrumb.Core;
using Common.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Octokit;
using ProductHeaderValue = Octokit.ProductHeaderValue;

namespace BlockcoreStatus.Controllers;

[BreadCrumb(Title = "Home", UseDefaultRouteUrl = true, Order = 0)]
public class HomeController : Controller
{
    private readonly IGithubService _github;
    private readonly IBlockcoreChainsService _chain;
    private readonly IBlockcoreIndexersService _indexer;


    public HomeController(IGithubService github, IBlockcoreChainsService chain, IBlockcoreIndexersService indexer)
    {
        _github = github ?? throw new ArgumentNullException(nameof(github));
        _chain = chain ?? throw new ArgumentNullException(nameof(chain));
        _indexer = indexer ?? throw new ArgumentNullException(nameof(indexer));

    }

    [BreadCrumb(Title = "Index", Order = 1)]
    public async Task<IActionResult> Index()
    {

        var orgs = await _github.GetAllOrganizationFromDB();
        List<string> OrganizationsList = new List<string>();
        foreach (var item in orgs)
        {
            OrganizationsList.Add(item.Login);
        }
        var chains = await _chain.GetAllChains();
        var indexers = await _indexer.GetAllIndexer();


        var model = new HomeViewModel()
        {
            Organizations = OrganizationsList,
            Chains = chains,
            Indexers = indexers
        };
        return View(model);
    }

    [NoBrowserCache]
    [HttpPost]
    [AjaxOnly]
    public async Task<IActionResult> Indexers([FromBody] PageViewModel model)
    {
        var pageNumber = model.Page ?? 1;
        //var indexers = await _indexer.GetIndexerFromDB(pageNumber);
        var indexers = await _indexer.GetAllIndexer();

        if (indexers == null || !indexers.Any())
            return Content("no-more-info");
        return PartialView("_IndexerList", indexers);

    }
    [AjaxOnly]
    public async Task<IActionResult> IndexersMarker()
    {
        var indexers = await _indexer.GetAllIndexer();
        var IndexersList = new List<BlockcoreIndexers>();
        foreach (var item in indexers)
        {
            IndexersList.AddRange(item.Indexers);
        }

        var model = IndexersList.Where(c => c.Online).GroupBy(c => c.Query).Select(g => new
        {
            GroupId = g.Key,
            Count = g.Count(),
            Location = g.Select(c => new { c.Lat, c.Lon, c.Query }).FirstOrDefault()
        }).ToList();
        return Json(model);

    }

    [BreadCrumb(Title = "Error", Order = 1)]
    public IActionResult Error()
    {
        return View();
    }


    [Authorize]
    public IActionResult CallBackResult(long token, string status, string orderId, string terminalNo, string rrn)
    {
        var userId = User.Identity?.GetUserId();
        return Json(new { userId, token, status, orderId, terminalNo, rrn });
    }
}