using blockcore.status.Entities.Github;
using blockcore.status.ViewModels.Github;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blockcore.status.Services.Contracts;
public interface IGithubService
{
    //get data from github with Octokit
    Task<Organization> GetOrganizationInfo(string login);
    Task<IReadOnlyList<Repository>> GetAllPublicRepositories(string owner);
    Task<Repository> GetRepositoryInfo(string owner, string name);
    Task<Release> GetLatestRepositoryRelease(string owner, string name);
    //-------------------------------------------------------------------

    Task<bool> AddOrganization(OrganizationViewModel org);
    Task<bool> EditOrganization(OrganizationViewModel org);
    Task<bool> DeleteOrganization(OrganizationViewModel org);
    Task<GithubOrganization> GetOrganizationById(int id);
    Task<GithubOrganization> GetOrganizationByName(string login, bool withRepositories = true);
    Task<GithubRepository> GetRepositoryByName(string owner, string name);
    Task<IReadOnlyList<GithubOrganization>> GetAllOrganization();
    Task<IReadOnlyList<GithubRepository>> GetAllRepositories(int orgId, int page = 1, int pageSize = 10);
    Task<bool> GetRepositoryInOrganization(int orgId);
    Task<bool> GetRepositoryInfoInOrganization(int orgId);

    Task<bool> UpdateOrganizationsRepositoriesSelected(string[] repositories, int orgId);

}
