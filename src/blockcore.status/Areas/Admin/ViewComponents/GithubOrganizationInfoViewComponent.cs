using blockcore.status.Services.Contracts;
using blockcore.status.Services.Contracts.Admin;
using blockcore.status.ViewModels.Admin;
using blockcore.status.ViewModels.Github;
using Microsoft.AspNetCore.Mvc;

namespace blockcore.status.Areas.Admin.ViewComponents;

public class GithubOrganizationInfoViewComponent : ViewComponent
{
    private readonly IGithubService _github;

    public GithubOrganizationInfoViewComponent(IGithubService github)
    {
        _github = github;
    }

    public async Task<IViewComponentResult> InvokeAsync(string OrgName)
    {
        var org = await _github.GetOrganizationInfo(OrgName);
        if (org == null)
        {
            return View("~/Areas/Admin/Views/Shared/Components/Github/OrganizationInfo.cshtml", new OrganizationInfoViewModel());
        }
        return View("~/Areas/Admin/Views/Shared/Components/Github/OrganizationInfo.cshtml",
            new OrganizationInfoViewModel
            {
                name = org.Name,
                description = org.Description,  
                avatarUrl = org.AvatarUrl,
                blog=org.Blog,
                login=org.Login,
                apiurl = org.Url
            });
    }
}