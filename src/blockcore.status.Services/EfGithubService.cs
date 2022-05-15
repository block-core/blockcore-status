using blockcore.status.DataLayer.Context;
using blockcore.status.Entities;
using blockcore.status.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Octokit;
using ProductHeaderValue = Octokit.ProductHeaderValue;

namespace blockcore.status.Services;

public class EfGithubService : IGithubService
{
    private readonly IUnitOfWork _uow;
    private readonly GitHubClient github;

    public EfGithubService(IUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
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

}