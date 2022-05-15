using blockcore.status.Entities.Github;
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


    Task<bool> AddOrganization(GithubOrganization org);
    Task<bool> EditOrganization(GithubOrganization org);
    Task<bool> DeleteOrganization(GithubOrganization org);
    Task<GithubOrganization> GetOrganization(int id);
    Task<IReadOnlyList<GithubOrganization>> GetAllOrganization();


}
