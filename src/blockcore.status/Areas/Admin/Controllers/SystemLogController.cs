using blockcore.status.Services.Contracts.Admin;
using blockcore.status.Services.Admin;
using blockcore.status.ViewModels.Admin;
using BreadCrumb.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blockcore.status.Areas.Identity.Controllers;

[Authorize(Roles = ConstantRoles.Admin), Area(AreaConstants.AdminArea),
 BreadCrumb(Title = "System Log", UseDefaultRouteUrl = true, Order = 0)]
public class SystemLogController : Controller
{
    private readonly IAppLogItemsService _appLogItemsService;

    public SystemLogController(
        IAppLogItemsService appLogItemsService)
    {
        _appLogItemsService = appLogItemsService ?? throw new ArgumentNullException(nameof(appLogItemsService));
    }

    [BreadCrumb(Title = "Index", Order = 1)]
    public async Task<IActionResult> Index(
        string logLevel = "",
        int pageNumber = 1,
        int pageSize = -1,
        string sort = "desc")
    {
        var itemsPerPage = 10;
        if (pageSize > 0)
        {
            itemsPerPage = pageSize;
        }

        var model = await _appLogItemsService.GetPagedAppLogItemsAsync(
            pageNumber, itemsPerPage,
            string.Equals(sort, "desc", StringComparison.OrdinalIgnoreCase)
                ? SortOrder.Descending
                : SortOrder.Ascending, logLevel);
        model.LogLevel = logLevel;
        model.Paging.CurrentPage = pageNumber;
        model.Paging.ItemsPerPage = itemsPerPage;
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> LogItemDelete(int id)
    {
        await _appLogItemsService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> LogDeleteAll(string logLevel = "")
    {
        await _appLogItemsService.DeleteAllAsync(logLevel);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> LogDeleteOlderThan(string logLevel = "", int days = 5)
    {
        var cutoffUtc = DateTime.UtcNow.AddDays(-days);
        await _appLogItemsService.DeleteOlderThanAsync(cutoffUtc, logLevel);
        return RedirectToAction(nameof(Index));
    }
}