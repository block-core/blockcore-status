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
    Task<Organization> GetOrganizationInfoFromGithub(string login);
    Task<IReadOnlyList<Repository>> GetAllRepositoriesFromGithub(string owner);
    Task<Repository> GetRepositoryInfoFromGithub(string owner, string name);
    Task<Release> GetLatestRepositoryReleaseFromGithub(string owner, string name);
    //-------------------------------------------------------------------

    Task<bool> AddOrganizationToDB(OrganizationViewModel org);
    Task<bool> EditOrganizationFromDB(OrganizationViewModel org);
    Task<bool> DeleteOrganizationFromDB(OrganizationViewModel org);

   
    Task<bool> AddRepositoriesInOrganizationToDB(int orgId);
    Task<bool> UpdateRepositoriesInfoInOrganization(int orgId);
    Task<bool> UpdateOrganizationsRepositoriesSelected(string[] repositories, int orgId);


    Task<GithubOrganization> GetOrganizationById(int id);
    Task<GithubOrganization> GetOrganizationByName(string login, bool withRepositories = true);
    Task<GithubRepository> GetRepositoryByName(string owner, string name);

   
    Task<IReadOnlyList<GithubOrganization>> GetAllOrganization();
    Task<IReadOnlyList<GithubRepository>> GetAllRepositories(int orgId, int page = 1, int pageSize = 10);

}
