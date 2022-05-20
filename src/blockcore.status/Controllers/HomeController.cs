using blockcore.status.Services.Contracts;
using blockcore.status.ViewModels.Home;
using BreadCrumb.Core;
using Common.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using ProductHeaderValue = Octokit.ProductHeaderValue;

namespace blockcore.status.Controllers;

[BreadCrumb(Title = "Home", UseDefaultRouteUrl = true, Order = 0)]
public class HomeController : Controller
{
    private readonly IGithubService _github;

    public HomeController(IGithubService github)
    {
        _github = github ?? throw new ArgumentNullException(nameof(github));
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
        var model = new HomeViewModel() { 
        Organizations=OrganizationsList
        };
        return View(model);
    }

    [BreadCrumb(Title = "Error", Order = 1)]
    public IActionResult Error()
    {
        return View();
    }

    /// <summary>
    ///     To test automatic challenge after redirecting from another site
    ///     Sample URL: http://localhost:5000/Home/CallBackResult?token=1&status=2&orderId=3&terminalNo=4&rrn=5
    /// </summary>
    [Authorize]
    public IActionResult CallBackResult(long token, string status, string orderId, string terminalNo, string rrn)
    {
        var userId = User.Identity?.GetUserId();
        return Json(new { userId, token, status, orderId, terminalNo, rrn });
    }
}