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
    Task<Repository> GetRepositoryInfo(string owner, string login);
    Task<Release> GetLatestRepositoryRelease(string owner, string login);
    //-------------------------------------------------------------------

    Task<bool> AddOrganization(OrganizationViewModel org);
    Task<bool> EditOrganization(OrganizationViewModel org);
    Task<bool> DeleteOrganization(OrganizationViewModel org);
    Task<GithubOrganization> GetOrganizationById(int id);
    Task<GithubOrganization> GetOrganizationByName(string login);
    Task<IReadOnlyList<GithubOrganization>> GetAllOrganization();
    Task<bool> GetRepositoryInOrganization(int orgId);
    Task<bool> GetRepositoryInfoInOrganization(int orgId);

    Task<bool> UpdateOrganizationsRepositoriesSelected(string[] repositories, int orgId);

}
