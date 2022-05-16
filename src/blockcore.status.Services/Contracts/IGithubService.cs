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
    Task<Organization> GetOrganizationInfo(string name);
    Task<IReadOnlyList<Repository>> GetAllPublicRepositories(string owner);
    Task<Repository> GetRepositoryInfo(string owner, string name);
    Task<Release> GetLatestRepositoryRelease(string owner, string name);


    Task<bool> AddOrganization(OrganizationViewModel org);
    Task<bool> EditOrganization(OrganizationViewModel org);
    Task<bool> DeleteOrganization(OrganizationViewModel org);
    Task<GithubOrganization> GetOrganizationById(int id);
    Task<GithubOrganization> GetOrganizationByName(string name);
    Task<IReadOnlyList<GithubOrganization>> GetAllOrganization();

    Task<bool> UpdateOrganizationsRepositories(string[] repositories, int orgId);

}
