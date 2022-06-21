using BlockcoreStatus.Entities.Github;
using BlockcoreStatus.ViewModels.Github;
using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockcoreStatus.Services.Contracts;
public interface IGithubService
{
    //get data from github with Octokit
    Task<Organization> GetOrganizationInfoFromGithub(string login);
    Task<IReadOnlyList<Repository>> GetAllRepositoriesFromGithub(string owner);
    Task<Repository> GetRepositoryInfoFromGithub(string owner, string name);
    Task<Release> GetLatestRepositoryReleaseFromGithub(string owner, string name);
    //-------------------------------------------------------------------

    Task<bool> AddOrganizationToDB(OrganizationViewModel org);
    Task<bool> UpdateOrganizationInDB(string login);
    Task<bool> DeleteOrganizationFromDB(OrganizationViewModel org);
    Task<GithubOrganization> GetOrganizationById(int id);
    Task<GithubOrganization> GetOrganizationByName(string login, bool withRepositories = true);
    Task<IReadOnlyList<GithubOrganization>> GetAllOrganizationFromDB();


    Task<bool> AddRepositoriesToDB(int orgId);
    Task<bool> UpdateRepositoriesInDB(int orgId);
    Task<bool> UpdateReposSelectedForShow(string[] repositories, int orgId);
    Task<IReadOnlyList<GithubRepository>> GetAllRepositoriesFromDB(int orgId);

    Task<IReadOnlyList<GithubRepository>> GetAllRepositoriesFromDB(int orgId, int page, int pageSize = 10);
    Task<GithubRepository> GetRepositoryByNameFromDB(string owner, string name);




    Task<bool> AddLatestRepositoryReleaseToDB(string owner, string name);
    Task<bool> UpdateLatestRepositoryReleaseInDB(string owner, string name);
    Task<GithubRelease> GetLatestRepositoryReleaseFromDB(string owner, string name);

    Task<bool> UpdateLatestRepositoriesReleaseInDB(string owner);

}
