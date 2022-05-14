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

    public EfGithubService(IUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));

    }

    public async Task<Organization> GetOrganizationInfo(string name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        var github = new GitHubClient(new ProductHeaderValue("blockcore"));

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
}