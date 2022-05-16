using blockcore.status.DataLayer.Context;
using blockcore.status.Entities;
using blockcore.status.Entities.Github;
using blockcore.status.Services.Contracts;
using blockcore.status.ViewModels.Github;
using Microsoft.EntityFrameworkCore;
using Octokit;
using ProductHeaderValue = Octokit.ProductHeaderValue;

namespace blockcore.status.Services;

public class EfGithubService : IGithubService
{
    private readonly DbSet<GithubOrganization> githubOrganizations;
    private readonly DbSet<GithubRepository> githubRepositories;
    private readonly IUnitOfWork _uow;
    private readonly GitHubClient github;

    public EfGithubService(IUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));

        githubOrganizations = _uow.Set<GithubOrganization>();
        githubRepositories = _uow.Set<GithubRepository>();
        github = new GitHubClient(new ProductHeaderValue("blockcore"));
    }

    public async Task<Organization> GetOrganizationInfo(string name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        try
        {
            var organization = await github.Organization.Get(name);
            return organization;
        }
        catch
        {
            return null;
        }


    }

    public async Task<Repository> GetRepositoryInfo(string owner, string name)
    {
        if (owner == null)
        {
            throw new ArgumentNullException(nameof(owner));
        }
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        try
        {
            var repository = await github.Repository.Get(owner, name);
            return repository;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Release> GetLatestRepositoryRelease(string owner, string name)
    {
        if (owner == null)
        {
            throw new ArgumentNullException(nameof(owner));
        }
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        try
        {
            var releases = await github.Repository.Release.GetLatest(owner, name);
            return releases;
        }
        catch
        {
            return null;
        }
    }

    public async Task<IReadOnlyList<Repository>> GetAllPublicRepositories(string owner)
    {
        if (owner == null)
        {
            throw new ArgumentNullException(nameof(owner));
        }
        try
        {
            var releases = await github.Repository.GetAllForOrg(owner);
            return releases;
        }
        catch
        {
            return null;
        }
    }



    public async Task<bool> AddOrganization(OrganizationViewModel org)
    {
        if (org == null)
        {
            throw new ArgumentNullException(nameof(org));
        }
        try
        {
            var organization = await GetOrganizationInfo(org.Login).ConfigureAwait(false);
            if (organization != null)
            {
                githubOrganizations.Add(new GithubOrganization()
                {
                    Name = organization.Name,
                    AvatarUrl = organization.AvatarUrl,
                    Blog = organization.Blog,
                    Company = organization.Company,
                    CreatedAt = organization.CreatedAt.UtcDateTime,
                    Description = organization.Description,
                    Email = organization.Email,
                    EventsUrl = organization.EventsUrl,
                    Followers = organization.Followers,
                    Following = organization.Following,
                    HasOrganizationProjects = organization.HasOrganizationProjects,
                    HasRepositoryProjects = organization.HasRepositoryProjects,
                    HooksUrl = organization.HooksUrl,
                    HtmlUrl = organization.HtmlUrl,
                    Id = organization.Id,
                    IssuesUrl = organization.IssuesUrl,
                    IsVerified = organization.IsVerified,
                    LatestDataUpdate = DateTime.UtcNow,
                    Location = organization.Location,
                    Login = organization.Login,
                    MembersUrl = organization.MembersUrl,
                    PublicGists = organization.PublicGists,
                    PublicMembersUrl = organization.PublicMembersUrl,
                    PublicRepos = organization.PublicRepos,
                    ReposUrl = organization.ReposUrl,
                    Type = organization.Type.ToString(),
                    UpdatedAt = organization.UpdatedAt.UtcDateTime,
                    Url = organization.Url
                });
                await _uow.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> EditOrganization(OrganizationViewModel org)
    {
        if (org == null)
        {
            throw new ArgumentNullException(nameof(org));
        }
        try
        {

            var organization = await GetOrganizationInfo(org.Login).ConfigureAwait(false);
            if (organization != null)
            {
                var oldorganization= await GetOrganizationById(org.OrganizationId).ConfigureAwait(false);
                oldorganization.Name = organization.Name;
                oldorganization.AvatarUrl = organization.AvatarUrl;
                oldorganization.Blog = organization.Blog;
                oldorganization.Company = organization.Company;
                oldorganization.CreatedAt = organization.CreatedAt.UtcDateTime;
                oldorganization.Description = organization.Description;
                oldorganization.Email = organization.Email;
                oldorganization.EventsUrl = organization.EventsUrl;
                oldorganization.Followers = organization.Followers;
                oldorganization.Following = organization.Following;
                oldorganization.HasOrganizationProjects = organization.HasOrganizationProjects;
                oldorganization.HasRepositoryProjects = organization.HasRepositoryProjects;
                oldorganization.HooksUrl = organization.HooksUrl;
                oldorganization.HtmlUrl = organization.HtmlUrl;
                oldorganization.Id = organization.Id;
                oldorganization.IssuesUrl = organization.IssuesUrl;
                oldorganization.IsVerified = organization.IsVerified;
                oldorganization.LatestDataUpdate = DateTime.UtcNow;
                oldorganization.Location = organization.Location;
                oldorganization.Login = organization.Login;
                oldorganization.MembersUrl = organization.MembersUrl;
                oldorganization.PublicGists = organization.PublicGists;
                oldorganization.PublicMembersUrl = organization.PublicMembersUrl;
                oldorganization.PublicRepos = organization.PublicRepos;
                oldorganization.ReposUrl = organization.ReposUrl;
                oldorganization.Type = organization.Type.ToString();
                oldorganization.UpdatedAt = organization.UpdatedAt.UtcDateTime;
                oldorganization.Url = organization.Url;

                githubOrganizations.Update(oldorganization);
                await _uow.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteOrganization(OrganizationViewModel org)
    {
        if (org == null)
        {
            throw new ArgumentNullException(nameof(org));
        }
        try
        {
            var organization =await githubOrganizations.Where(c=>c.GithubOrganizationId==org.OrganizationId).Select(c=>c).FirstOrDefaultAsync();
            githubOrganizations.Remove(organization);
            await _uow.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<GithubOrganization> GetOrganizationById(int id)
    {
        if (id == 0)
        {
            throw new ArgumentNullException(nameof(id));
        }
        return await githubOrganizations.Where(c => c.GithubOrganizationId == id).Include(c => c.GithubRepository).Select(c => c).FirstOrDefaultAsync();
    }


    public async Task<GithubOrganization> GetOrganizationByName(string name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        return await githubOrganizations.Where(c => c.Login == name).Include(c => c.GithubRepository).Select(c => c).FirstOrDefaultAsync();
    }


    public async Task<IReadOnlyList<GithubOrganization>> GetAllOrganization()
    {
        return await githubOrganizations.ToListAsync();
    }


    public async Task<bool> UpdateOrganizationsRepositories(string[] repositories, int orgId)
    {
        if (repositories == null)
        {
            throw new ArgumentNullException(nameof(repositories));
        }
        if (orgId == 0)
        {
            throw new ArgumentNullException(nameof(orgId));
        }
        try
        {
            var repos = await githubRepositories.Where(c => c.GithubOrganizationId == orgId).ToListAsync();
            foreach (var item in repos)
            {
                item.IsSelect = false;
                githubRepositories.Update(item);
            }
            await _uow.SaveChangesAsync();

            foreach (var item in repositories)
            {
                var repo = await githubRepositories.Where(c => c.Name == item).FirstOrDefaultAsync();

                repo.IsSelect = true;
                githubRepositories.Update(repo);
            }
            await _uow.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;

        }
    }



}