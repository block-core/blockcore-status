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
        var org = await _github.GetOrganizationByName(OrgName);

        return View("~/Areas/Admin/Views/Shared/Components/Github/OrganizationInfo.cshtml",
            new OrganizationInfoViewModel
            {
                Name = org.Name,
                Description = org.Description,
                AvatarUrl = org.AvatarUrl,
                Blog = org.Blog,
                Login = org.Login,
                Apiurl = org.Url,
                Repositories = org.GithubRepositories.Where(c => c.IsSelect).Select(c => new RepositoryInfoViewModel()
                {
                    LastVersion = c.GithubRelease == null ? " - " : c.GithubRelease.TagName,
                    Name = c.Name,
                    RepositoryURL = c.HtmlUrl,
                    UpdatedAt = c.UpdatedAt,
                }).ToList()
            });
    }

}