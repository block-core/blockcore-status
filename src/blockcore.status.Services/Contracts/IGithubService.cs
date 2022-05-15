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
    Task<Repository> GetRepositoryInfo(string owner, string name);
    Task<Release> GetLatestRepositoryRelease(string owner, string name);

}
