using blockcore.status.DataLayer.Context;
using blockcore.status.Entities;
using blockcore.status.Entities.Github;
using blockcore.status.Services.Contracts;
using blockcore.status.ViewModels.Github;
using Microsoft.EntityFrameworkCore;
using Octokit;
using ProductHeaderValue = Octokit.ProductHeaderValue;
using System.Linq;

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

    public async Task<Organization> GetOrganizationInfo(string login)
    {
        if (login == null)
        {
            throw new ArgumentNullException(nameof(login));
        }
        try
        {
            var organization = await github.Organization.Get(login);
            return organization;
        }
        catch
        {
            return null;
        }


    }

    public async Task<Repository> GetRepositoryInfo(string owner, string login)
    {
        if (owner == null)
        {
            throw new ArgumentNullException(nameof(owner));
        }
        if (login == null)
        {
            throw new ArgumentNullException(nameof(login));
        }
        try
        {
            var repository = await github.Repository.Get(owner, login);
            return repository;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Release> GetLatestRepositoryRelease(string owner, string login)
    {
        if (owner == null)
        {
            throw new ArgumentNullException(nameof(owner));
        }
        if (login == null)
        {
            throw new ArgumentNullException(nameof(login));
        }
        try
        {
            var releases = await github.Repository.Release.GetLatest(owner, login);
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


    /// <summary>
    /// add organization
    /// </summary>
    /// <param name="org"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
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

    /// <summary>
    /// edit organization
    /// </summary>
    /// <param name="org"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
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
                var oldorganization = await GetOrganizationById(org.OrganizationId).ConfigureAwait(false);
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

    /// <summary>
    /// delete organization
    /// </summary>
    /// <param name="org"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<bool> DeleteOrganization(OrganizationViewModel org)
    {
        if (org == null)
        {
            throw new ArgumentNullException(nameof(org));
        }
        try
        {
            var organization = await githubOrganizations.Where(c => c.GithubOrganizationId == org.OrganizationId).Select(c => c).FirstOrDefaultAsync();
            githubOrganizations.Remove(organization);
            await _uow.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// get organization by Id (cointains repositories)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<GithubOrganization> GetOrganizationById(int id)
    {
        if (id == 0)
        {
            throw new ArgumentNullException(nameof(id));
        }
        return await githubOrganizations.Where(c => c.GithubOrganizationId == id).Include(c => c.GithubRepository).Select(c => c).FirstOrDefaultAsync();
    }

    /// <summary>
    /// get organization by name (cointains repositories)
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<GithubOrganization> GetOrganizationByName(string login)
    {
        if (login == null)
        {
            throw new ArgumentNullException(nameof(login));
        }
        return await githubOrganizations.Where(c => c.Login == login).Include(c => c.GithubRepository).Select(c => c).FirstOrDefaultAsync();
    }

    /// <summary>
    /// get all organization
    /// </summary>
    /// <returns></returns>
    public async Task<IReadOnlyList<GithubOrganization>> GetAllOrganization()
    {
        return await githubOrganizations.ToListAsync();
    }
    
    /// <summary>
    /// update repository selection for show in home page  
    /// </summary>
    /// <param name="repositories"></param>
    /// <param name="orgId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<bool> UpdateOrganizationsRepositoriesSelected(string[] repositories, int orgId)
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

    /// <summary>
    /// get All repository and save to DB for first time.
    /// </summary>
    /// <param name="orgId"></param>
    /// <returns></returns>
    public async Task<bool> GetRepositoryInOrganization(int orgId)
    {
        try
        {
            var org = await GetOrganizationById(orgId);
            var orgsreposname = org.GithubRepository.Select(c => c.Name).ToList();

            var publicRepos = await GetAllPublicRepositories(org.Login);

            foreach (var item in publicRepos)
            {
                if (!orgsreposname.Contains(item.Name))
                {
                    var newrepo = new GithubRepository()
                    {
                        GithubOrganizationId = org.GithubOrganizationId,
                        GitUrl = item.Name,
                        Name = item.Name,
                        HasDownloads = item.HasDownloads,
                        HasIssues = item.HasIssues,
                        Archived = item.Archived,
                        HasPages = item.HasPages,
                        HasWiki = item.HasWiki,
                        Homepage = item.Homepage,
                        HtmlUrl = item.HtmlUrl,
                        Id = item.Id,
                        IsSelect = false,
                        CloneUrl = item.CloneUrl,
                        CreatedAt = item.CreatedAt.UtcDateTime,
                        DefaultBranch = item.DefaultBranch,
                        Description = item.Description,
                        ForksCount = item.ForksCount,
                        FullName = item.FullName,
                        Language = item.Language,
                        MirrorUrl = item.MirrorUrl,
                        NodeId = item.NodeId,
                        LatestDataUpdate = DateTime.UtcNow,
                        WatchersCount = item.WatchersCount,
                        Url = item.Url,
                        UpdatedAt = item.UpdatedAt.UtcDateTime,
                        SvnUrl = item.SvnUrl,
                        StargazersCount = item.StargazersCount,
                        SshUrl = item.SshUrl,
                        Size = item.Size,
                        PushedAt = item.PushedAt.HasValue ? item.PushedAt.Value.UtcDateTime : null,
                        OpenIssuesCount = item.OpenIssuesCount
                    };
                    githubRepositories.Add(newrepo);
                }
            }
            await _uow.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// get and update repository information from github.
    /// </summary>
    /// <param name="orgId"></param>
    /// <returns></returns>
    public async Task<bool> GetRepositoryInfoInOrganization(int orgId)
    {
        try
        {
            var org = await GetOrganizationById(orgId);
            var orgsrepos = org.GithubRepository;

            var publicRepos = await GetAllPublicRepositories(org.Login);

            foreach (var pubRepo in publicRepos)
            {
                foreach (var localRepo in from localRepo in orgsrepos
                                          where string.Equals(pubRepo.Name, localRepo.Name, StringComparison.Ordinal)
                                          select localRepo)
                {
                    localRepo.GitUrl = pubRepo.Name;
                    localRepo.Name = pubRepo.Name;
                    localRepo.HasDownloads = pubRepo.HasDownloads;
                    localRepo.HasIssues = pubRepo.HasIssues;
                    localRepo.Archived = pubRepo.Archived;
                    localRepo.HasPages = pubRepo.HasPages;
                    localRepo.HasWiki = pubRepo.HasWiki;
                    localRepo.Homepage = pubRepo.Homepage;
                    localRepo.HtmlUrl = pubRepo.HtmlUrl;
                    localRepo.Id = pubRepo.Id;
                    localRepo.CloneUrl = pubRepo.CloneUrl;
                    localRepo.CreatedAt = pubRepo.CreatedAt.UtcDateTime;
                    localRepo.DefaultBranch = pubRepo.DefaultBranch;
                    localRepo.Description = pubRepo.Description;
                    localRepo.ForksCount = pubRepo.ForksCount;
                    localRepo.FullName = pubRepo.FullName;
                    localRepo.Language = pubRepo.Language;
                    localRepo.MirrorUrl = pubRepo.MirrorUrl;
                    localRepo.NodeId = pubRepo.NodeId;
                    localRepo.LatestDataUpdate = DateTime.UtcNow;
                    localRepo.WatchersCount = pubRepo.WatchersCount;
                    localRepo.Url = pubRepo.Url;
                    localRepo.UpdatedAt = pubRepo.UpdatedAt.UtcDateTime;
                    localRepo.SvnUrl = pubRepo.SvnUrl;
                    localRepo.StargazersCount = pubRepo.StargazersCount;
                    localRepo.SshUrl = pubRepo.SshUrl;
                    localRepo.Size = pubRepo.Size;
                    localRepo.PushedAt = pubRepo.PushedAt.HasValue ? pubRepo.PushedAt.Value.UtcDateTime : null;
                    localRepo.OpenIssuesCount = pubRepo.OpenIssuesCount;
                    githubRepositories.Update(localRepo);
                    await _uow.SaveChangesAsync();
                }
            }

            return true;
        }
        catch
        {
            return false;
        }
    }


}