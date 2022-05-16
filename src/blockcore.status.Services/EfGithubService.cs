using blockcore.status.DataLayer.Context;
using blockcore.status.Entities;
using blockcore.status.Entities.Github;
using blockcore.status.Services.Contracts;
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
            var releases = await github.Repository.GetAllForUser(owner);
            return releases;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> AddOrganization(GithubOrganization org)
    {
        if (org == null)
        {
            throw new ArgumentNullException(nameof(org));
        }
        try
        {
            githubOrganizations.Add(org);
            await _uow.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> EditOrganization(GithubOrganization org)
    {
        if (org == null)
        {
            throw new ArgumentNullException(nameof(org));
        }
        try
        {
            githubOrganizations.Update(org);
            await _uow.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteOrganization(GithubOrganization org)
    {
        if (org == null)
        {
            throw new ArgumentNullException(nameof(org));
        }
        try
        {
            githubOrganizations.Remove(org);
            await _uow.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<GithubOrganization> GetOrganization(int id)
    {
        if (id == 0)
        {
            throw new ArgumentNullException(nameof(id));
        }
        return await githubOrganizations.Where(c => c.Id == id).Select(c => c).FirstOrDefaultAsync();
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
            var repo = await githubRepositories.Where(c => c.OrganizationId == orgId).ToListAsync();
            foreach (var item in repo)
            {
                githubRepositories.Remove(item);
            }
            await _uow.SaveChangesAsync();

            foreach (var item in repositories)
            {
                var newrepo = new GithubRepository() { OrganizationId = orgId, CreatedDateTime = DateTime.UtcNow, RepositoryName = item };
                githubRepositories.Add(newrepo);
            }
            await _uow.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;

        }
    }

    public async Task<IReadOnlyList<GithubRepository>> GetOrganizationsRepositories(int orgId)
    {
        if (orgId == 0)
        {
            throw new ArgumentNullException(nameof(orgId));
        }
        return await githubRepositories.Where(c => c.OrganizationId == orgId).Select(c => c).ToListAsync();
    }
}