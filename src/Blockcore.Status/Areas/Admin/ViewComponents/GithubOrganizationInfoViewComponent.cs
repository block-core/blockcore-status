using BlockcoreStatus.Services.Contracts;
using BlockcoreStatus.Services.Contracts.Admin;
using BlockcoreStatus.ViewModels.Admin;
using BlockcoreStatus.ViewModels.Github;
using Microsoft.AspNetCore.Mvc;

namespace BlockcoreStatus.Areas.Admin.ViewComponents;

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
                APIurl = org.Url,
                HTMLurl = org.HtmlUrl,
                Repositories = org.GithubRepositories.Where(c => c.IsSelect).Select(c => new RepositoryInfoViewModel()
                {
                    LastVersion = c.GithubRelease == null ? " - " : c.GithubRelease.Name,
                    Name = c.Name,
                    RepositoryURL = c.HtmlUrl,
                    UpdatedAt = c.UpdatedAt,
                    PushedAt = c.PushedAt.Value,
                    OpenIssuesCount = c.OpenIssuesCount
                }).ToList()
            });
    }

}