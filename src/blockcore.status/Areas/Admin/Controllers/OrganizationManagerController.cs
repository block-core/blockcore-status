using blockcore.status.Common.IdentityToolkit;
using blockcore.status.Services.Contracts.Admin;
using blockcore.status.Services.Admin;
using blockcore.status.ViewModels.Admin;
using BreadCrumb.Core;
using Common.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using blockcore.status.Entities.Admin;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using blockcore.status.Services.Contracts;
using blockcore.status.Entities.Github;
using blockcore.status.ViewModels.Github;

namespace blockcore.status.Areas.Admin.Controllers;

[Authorize(Roles = ConstantRoles.Admin), Area(AreaConstants.AdminArea),
 BreadCrumb(Title = "Organization Manager", UseDefaultRouteUrl = true, Order = 0)]
public class OrganizationManagerController : Controller
{
    private const string OrganizationNotFound = "Organization Not Found.";
    //private const int DefaultPageSize = 7;

    private readonly IGithubService _githubService;

    public OrganizationManagerController(IGithubService githubService)
    {
        _githubService = githubService ?? throw new ArgumentNullException(nameof(githubService));
    }

   
    [DisplayName("Index"), BreadCrumb(Order = 1)]
    public async Task<IActionResult> Index()
    {
        var organizations = await _githubService.GetAllOrganization();
        return View(organizations);
    }

   
    [AjaxOnly]
    public async Task<IActionResult> RenderOrganization([FromBody] ModelIdViewModel model)
    {
        if (model is null)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (model.Id == 0)
        {
            return PartialView("_Create", new OrganizationViewModel() { OrganizationId = 0 });
        }

        var org = await _githubService.GetOrganizationById(model.Id);
        if (org == null)
        {
            ModelState.AddModelError("", OrganizationNotFound);
            return PartialView("_Create", new OrganizationViewModel());
        }

        return PartialView("_Create",
            new OrganizationViewModel { OrganizationId = org.GithubOrganizationId, Login = org.Login });
    }

  
    [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditOrganization(OrganizationViewModel model)
    {
        if (model is null)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var org = await _githubService.GetOrganizationById(model.OrganizationId);
            if (org == null)
            {
                ModelState.AddModelError("", OrganizationNotFound);
            }
            else
            {
              
                var result = await _githubService.EditOrganization(model);
                if (result)
                {
                    return Json(new { success = true });
                }

                ModelState.AddModelError("", "An error occurred while editing");
            }
        }

        return PartialView("_Create", model);
    }

   
    [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrganization(OrganizationViewModel model)
    {
        if (model is null)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var result = await _githubService.AddOrganization(model);
            if (result)
            {
                return Json(new { success = true });
            }

            ModelState.AddModelError("", "An error occurred while adding");
        }

        return PartialView("_Create", model);
    }

  
    [AjaxOnly]
    public async Task<IActionResult> RenderDeleteOrganization([FromBody] ModelIdViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (model == null)
        {
            return BadRequest("model is null.");
        }

        var org = await _githubService.GetOrganizationById(model.Id);
        if (org == null)
        {
            ModelState.AddModelError("", OrganizationNotFound);
            return PartialView("_Delete", new OrganizationViewModel());
        }

        return PartialView("_Delete",
            new OrganizationViewModel { OrganizationId = org.GithubOrganizationId, Login = org.Login });
    }

  
    [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(OrganizationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (model == null)
        {
            return BadRequest("model is null.");
        }

        var org = await _githubService.GetOrganizationById(model.OrganizationId);
        if (org == null)
        {
            ModelState.AddModelError("", OrganizationNotFound);
        }
        else
        {
            var result = await _githubService.DeleteOrganization(model);
            if (result)
            {
                return Json(new { success = true });
            }

            ModelState.AddModelError("", "An error occurred while deleting");
        }

        return PartialView("_Delete", model);
    }

    
    [BreadCrumb(Title = "List of Repository in Organization", Order = 1)]
    public async Task<IActionResult> RepositoryInOrganization(int? id)
    {
        if (id == null)
        {
            return View("Error");
        }
        var organization = await _githubService.GetOrganizationById(id.Value).ConfigureAwait(true);
        var model = organization.GithubRepositories;

        return View(model);
    }


    [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> RepositoryInOrganization(string[] Repositories, int OrgId)
    {

        var result = await _githubService.UpdateOrganizationsRepositoriesSelected(Repositories, OrgId);
        if (result)
        {
            return Json(new { success = true });
        }
        else
        {
            ModelState.AddModelError("", "An error occurred while Updating");
            return BadRequest("An error occurred while Updating");
        }

    }


    [AjaxOnly, HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> GetRepositoryInOrganization( int OrgId)
    {
        var result =await _githubService.GetRepositoryInOrganization(OrgId);
        if (result)
        {
            return Json(new { success = true });
        }
        else
        {
            ModelState.AddModelError("", "An error occurred while Updating");
            return BadRequest("An error occurred while Updating");
        }

    }
}



