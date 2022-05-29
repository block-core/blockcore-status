using blockcore.status.Common.IdentityToolkit;
using blockcore.status.Services.Contracts.Admin;
using blockcore.status.Services.Admin;
using blockcore.status.ViewModels.Admin;
using BreadCrumb.Core;
using Common.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blockcore.status.Areas.Admin.Controllers;

[Authorize(Roles = ConstantRoles.Admin), Area(AreaConstants.AdminArea),
 BreadCrumb(Title = "Dynamic Role Claims Manager", UseDefaultRouteUrl = true, Order = 0)]
public class DynamicRoleClaimsManagerController : Controller
{
    private readonly IMvcActionsDiscoveryService _mvcActionsDiscoveryService;
    private readonly IApplicationRoleManager _roleManager;

    public DynamicRoleClaimsManagerController(
        IMvcActionsDiscoveryService mvcActionsDiscoveryService,
        IApplicationRoleManager roleManager)
    {
        _mvcActionsDiscoveryService = mvcActionsDiscoveryService ??
                                      throw new ArgumentNullException(nameof(mvcActionsDiscoveryService));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    [BreadCrumb(Title = "Index", Order = 1)]
    public async Task<IActionResult> Index(int? id)
    {
        this.AddBreadCrumb(new BreadCrumb.Core.BreadCrumb
        {
            Title = "Role Manager",
            Url = Url.Action("Index", "RolesManager"),
            Order = -1
        });

        if (!id.HasValue)
        {
            return View("Error");
        }

        var role = await _roleManager.FindRoleIncludeRoleClaimsAsync(id.Value);
        if (role == null)
        {
            return View("NotFound");
        }

        var securedControllerActions =
            _mvcActionsDiscoveryService.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);
        return View(new DynamicRoleClaimsManagerViewModel
        {
            SecuredControllerActions = securedControllerActions,
            RoleIncludeRoleClaims = role
        });
    }

    [AjaxOnly, HttpPost, ValidateAntiForgeryToken, ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> Index(DynamicRoleClaimsManagerViewModel model)
    {
        if (model is null)
        {
            return View("Error");
        }

        var result = await _roleManager.AddOrUpdateRoleClaimsAsync(
            model.RoleId,
            ConstantPolicies.DynamicPermissionClaimType,
            model.ActionIds);
        if (!result.Succeeded)
        {
            return BadRequest(result.DumpErrors(true));
        }

        return Json(new { success = true });
    }
}