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
    private readonly DbSet<GithubRelease> githubRelease;

    private readonly IUnitOfWork _uow;
    private readonly GitHubClient github;

    public EfGithubService(IUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));

        githubOrganizations = _uow.Set<GithubOrganization>();
        githubRepositories = _uow.Set<GithubRepository>();
        githubRelease = _uow.Set<GithubRelease>();
        github = new GitHubClient(new ProductHeaderValue("blockcore"));
    }

    public async Task<Organization> GetOrganizationInfoFromGithub(string login)
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

    public async Task<Repository> GetRepositoryInfoFromGithub(string owner, string name)
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

    public async Task<Release> GetLatestRepositoryReleaseFromGithub(string owner, string name)
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

    public async Task<IReadOnlyList<Repository>> GetAllRepositoriesFromGithub(string owner)
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
    public async Task<bool> AddOrganizationToDB(OrganizationViewModel org)
    {
        if (org == null)
        {
            throw new ArgumentNullException(nameof(org));
        }
        try
        {
            var organization = await GetOrganizationInfoFromGithub(org.Login).ConfigureAwait(false);
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
    public async Task<bool> EditOrganizationFromDB(OrganizationViewModel org)
    {
        if (org == null)
        {
            throw new ArgumentNullException(nameof(org));
        }
        try
        {

            var organization = await GetOrganizationInfoFromGithub(org.Login).ConfigureAwait(false);
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
    public async Task<bool> DeleteOrganizationFromDB(OrganizationViewModel org)
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
        return await githubOrganizations.Where(c => c.GithubOrganizationId == id).Include(c => c.GithubRepositories).Select(c => c).FirstOrDefaultAsync();
    }

    /// <summary>
    /// get organization by name (cointains repositories)
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<GithubOrganization> GetOrganizationByName(string login, bool withRepositories = true)
    {
        if (login == null)
        {
            throw new ArgumentNullException(nameof(login));
        }
        if (withRepositories)
        {
            return await githubOrganizations.Where(c => c.Login == login).Include(c => c.GithubRepositories).ThenInclude(c=>c.GithubRelease).Select(c => c).FirstOrDefaultAsync();

        }

        else
        {
            return await githubOrganizations.Where(c => c.Login == login).Select(c => c).FirstOrDefaultAsync();

        }
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
    public async Task<bool> UpdateReposSelected(string[] repositories, int orgId)
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

                //get latest release

                var org = await githubOrganizations.Where(c => c.GithubOrganizationId == orgId).FirstOrDefaultAsync();
                if (repo.GithubRelease != null)
                {
                    await UpdateLatestRepositoryRelease(org.Name, repo.Name);
                }
                else
                {
                    await AddLatestRepositoryRelease(org.Login, repo.Name);
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
    /// get All repository and save to DB for first time.
    /// </summary>
    /// <param name="orgId"></param>
    /// <returns></returns>
    public async Task<bool> GetFromGithubAndAddReposToDB(int orgId)
    {
        if (orgId == 0)
        {
            throw new ArgumentNullException(nameof(orgId));
        }

        try
        {
            var org = await GetOrganizationById(orgId);
            var orgsreposname = org.GithubRepositories.Select(c => c.Name).ToList();

            var allRepos = await GetAllRepositoriesFromGithub(org.Login);

            foreach (var item in allRepos)
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
    public async Task<bool> UpdateReposInfoAsync(int orgId)
    {
        if (orgId == 0)
        {
            throw new ArgumentNullException(nameof(orgId));
        }
        try
        {
            var org = await GetOrganizationById(orgId);
            var orgsRepos = org.GithubRepositories;
            var publicRepos = await GetAllRepositoriesFromGithub(org.Login);

            foreach (var pubRepo in publicRepos)
            {
                foreach (var localRepo in from localRepo in orgsRepos
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


    /// <summary>
    /// get repository by name
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<GithubRepository> GetRepositoryByName(string owner, string name)
    {
        if (owner == null)
        {
            throw new ArgumentNullException(nameof(owner));
        }
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        var org = await GetOrganizationByName(owner);
        var allrepos = org.GithubRepositories.ToList();
        var repo = allrepos.Select(c => new GithubRepository()
        {
            GithubOrganizationId = c.GithubOrganizationId,
            Name = c.Name,
            Archived = c.Archived,
            CloneUrl = c.CloneUrl,
            CreatedAt = c.CreatedAt,
            DefaultBranch = c.DefaultBranch,
            Description = c.Description,
            ForksCount = c.ForksCount,
            FullName = c.FullName,
            GitUrl = c.GitUrl,
            HasDownloads = c.HasDownloads,
            HasIssues = c.HasIssues,
            HasPages = c.HasPages,
            HasWiki = c.HasWiki,
            Homepage = c.Homepage,
            Id = c.Id,
            HtmlUrl = c.HtmlUrl,
            Language = c.Language,
            LatestDataUpdate = c.LatestDataUpdate,
            MirrorUrl = c.MirrorUrl,
            NodeId = c.NodeId,
            OpenIssuesCount = c.OpenIssuesCount,
            PushedAt = c.PushedAt,
            Size = c.Size,
            SshUrl = c.SshUrl,
            StargazersCount = c.StargazersCount,
            SvnUrl = c.SvnUrl,
            UpdatedAt = c.UpdatedAt,
            WatchersCount = c.WatchersCount,
            Url = c.Url,
            GithubRelease = new GithubRelease()
            {
                AssetsUrl = c.GithubRelease.AssetsUrl,
                Body = c.GithubRelease.Body,
                CreatedAt = c.GithubRelease.CreatedAt,
                Draft = c.GithubRelease.Draft,
                HtmlUrl = c.GithubRelease.HtmlUrl,
                Id = c.GithubRelease.Id,
                Name = c.GithubRelease.Name,
                LatestDataUpdate = c.GithubRelease.LatestDataUpdate,
                NodeId = c.GithubRelease.NodeId,
                Prerelease = c.GithubRelease.Prerelease,
                PublishedAt = c.GithubRelease.PublishedAt,
                TagName = c.GithubRelease.TagName,
                TarballUrl = c.GithubRelease.TarballUrl,
                TargetCommitish = c.GithubRelease.TargetCommitish,
                UploadUrl = c.GithubRelease.UploadUrl,
                Url = c.GithubRelease.Url,
                ZipballUrl = c.GithubRelease.ZipballUrl,
            }  

        }).FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.Ordinal));

        if (repo == null)
            return null;
        else
            return repo;
    }

    /// <summary>
    /// get all repositories 
    /// </summary>
    /// <param name="orgId"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<IReadOnlyList<GithubRepository>> GetAllRepositories(int orgId, int page = 1, int pageSize = 10)
    {
        if (orgId == 0)
        {
            throw new ArgumentNullException(nameof(orgId));
        }

        return await githubRepositories.Where(c => c.GithubOrganizationId == orgId).Take(page * pageSize).Skip((page - 1) * pageSize).Select(c => new GithubRepository()
        {
            GithubOrganizationId = c.GithubOrganizationId,
            Name = c.Name,
            Archived = c.Archived,
            CloneUrl = c.CloneUrl,
            CreatedAt = c.CreatedAt,
            DefaultBranch = c.DefaultBranch,
            Description = c.Description,
            ForksCount = c.ForksCount,
            FullName = c.FullName,
            GitUrl = c.GitUrl,
            HasDownloads = c.HasDownloads,
            HasIssues = c.HasIssues,
            HasPages = c.HasPages,
            HasWiki = c.HasWiki,
            Homepage = c.Homepage,
            Id = c.Id,
            HtmlUrl = c.HtmlUrl,
            Language = c.Language,
            LatestDataUpdate = c.LatestDataUpdate,
            MirrorUrl = c.MirrorUrl,
            NodeId = c.NodeId,
            OpenIssuesCount = c.OpenIssuesCount,
            PushedAt = c.PushedAt,
            Size = c.Size,
            SshUrl = c.SshUrl,
            StargazersCount = c.StargazersCount,
            SvnUrl = c.SvnUrl,
            UpdatedAt = c.UpdatedAt,
            WatchersCount = c.WatchersCount,
            Url = c.Url

        }).ToListAsync();

    }


    /// <summary>
    /// add latest repository release
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> AddLatestRepositoryRelease(string owner, string name)
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
            var org = await GetOrganizationByName(owner);
            if (org != null)
            {
                var repo = org.GithubRepositories.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.Ordinal));
                if (repo != null)
                {
                    var latestRelease = await GetLatestRepositoryReleaseFromGithub(owner, name);
                    if (latestRelease != null)
                    {
                        await githubRelease.AddAsync(new GithubRelease()
                        {
                            GithubRepositoryId = repo.GithubRepositoryId,
                            Name = latestRelease.Name,
                            Id = latestRelease.Id,
                            AssetsUrl = latestRelease.AssetsUrl,
                            Body = latestRelease.Body,
                            CreatedAt = latestRelease.CreatedAt.UtcDateTime,
                            Draft = latestRelease.Draft,
                            HtmlUrl = latestRelease.HtmlUrl,
                            LatestDataUpdate = DateTime.UtcNow,
                            NodeId = latestRelease.NodeId,
                            Prerelease = latestRelease.Prerelease,
                            PublishedAt = latestRelease.PublishedAt.HasValue ? latestRelease.PublishedAt.Value.UtcDateTime : null,
                            ZipballUrl = latestRelease.ZipballUrl,
                            TagName = latestRelease.TagName,
                            TarballUrl = latestRelease.TarballUrl,
                            TargetCommitish = latestRelease.TargetCommitish,
                            UploadUrl = latestRelease.UploadUrl,
                            Url = latestRelease.Url,
                        });
                        await _uow.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
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

    public async Task<bool> UpdateLatestRepositoryRelease(string owner, string name)
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
            var org = await GetOrganizationByName(owner);
            if (org != null)
            {
                var repo = org.GithubRepositories.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.Ordinal));
                if (repo != null)
                {
                    var release = repo.GithubRelease;
                    if (release != null)
                    {
                        var latestRelease = await GetLatestRepositoryReleaseFromGithub(owner, name);
                        if (latestRelease != null)
                        {
                            release.GithubRepositoryId = repo.GithubRepositoryId;
                            release.Name = latestRelease.Name;
                            release.Id = latestRelease.Id;
                            release.AssetsUrl = latestRelease.AssetsUrl;
                            release.Body = latestRelease.Body;
                            release.CreatedAt = latestRelease.CreatedAt.UtcDateTime;
                            release.Draft = latestRelease.Draft;
                            release.HtmlUrl = latestRelease.HtmlUrl;
                            release.LatestDataUpdate = DateTime.UtcNow;
                            release.NodeId = latestRelease.NodeId;
                            release.Prerelease = latestRelease.Prerelease;
                            release.PublishedAt = latestRelease.PublishedAt.HasValue ? latestRelease.PublishedAt.Value.UtcDateTime : null;
                            release.ZipballUrl = latestRelease.ZipballUrl;
                            release.TagName = latestRelease.TagName;
                            release.TarballUrl = latestRelease.TarballUrl;
                            release.TargetCommitish = latestRelease.TargetCommitish;
                            release.UploadUrl = latestRelease.UploadUrl;
                            release.Url = latestRelease.Url;

                            githubRelease.Update(release);

                            await _uow.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            githubRelease.Remove(release);
                            await _uow.SaveChangesAsync();
                            return true;
                        }
                    }
                    else
                    {

                        return false;
                    }
                }
                else
                {
                    return false;
                }
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

    public async Task<GithubRelease> GetLatestRepositoryRelease(string owner, string name)
    {
        if (owner == null)
        {
            throw new ArgumentNullException(nameof(owner));
        }
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        var org = await GetOrganizationByName(owner);
        if (org != null)
        {     
            var repo =await GetRepositoryByName(owner, name);

            if (repo != null)
            {
                var release = new GithubRelease() {

                   AssetsUrl= repo.GithubRelease.AssetsUrl,
                   Body = repo.GithubRelease.Body,
                   CreatedAt = repo.GithubRelease.CreatedAt,
                   Draft= repo.GithubRelease.Draft,
                   HtmlUrl= repo.GithubRelease.HtmlUrl,
                   Id= repo.GithubRelease.Id,
                   Name= repo.GithubRelease.Name,
                   LatestDataUpdate= repo.GithubRelease.LatestDataUpdate,
                   NodeId= repo.GithubRelease.NodeId,
                   Prerelease= repo.GithubRelease.Prerelease,
                   PublishedAt= repo.GithubRelease.PublishedAt,
                   TagName= repo.GithubRelease.TagName,
                   TarballUrl= repo.GithubRelease.TarballUrl,
                   TargetCommitish= repo.GithubRelease.TargetCommitish,
                   UploadUrl= repo.GithubRelease.UploadUrl,
                   Url= repo.GithubRelease.Url,
                   ZipballUrl= repo.GithubRelease.ZipballUrl,
            };
                return release;
            }
        }
        return null;
    }

}