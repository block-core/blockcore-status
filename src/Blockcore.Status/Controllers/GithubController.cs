﻿using BlockcoreStatus.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Octokit;
using System.Threading.Tasks;


namespace BlockcoreStatus.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GithubController : Controller
{
    private readonly IGithubService _github;

    public GithubController(IGithubService github)
    {
        _github = github ?? throw new ArgumentNullException(nameof(github));
    }


    [HttpGet("[action]/{name}")]
    public async Task<IActionResult> OrganizationInfo(string name)
    {
        var org = await _github.GetOrganizationByName(name, false);
        if (org == null)
        {
            return NotFound();
        }
        return Ok(org);
    }


    [HttpGet("[action]/{owner}/{page}")]
    public async Task<IActionResult> Repositories(string owner, int page = 1)
    {
        var orgInfo = await _github.GetOrganizationByName(owner, false);
        if (orgInfo != null)
        {
            var repos = await _github.GetAllRepositoriesFromDB(orgInfo.GithubOrganizationId, page);

            if (repos != null)
            {
                return Ok(repos);
            }
        }
        return NotFound();


    }


    [HttpGet("[action]/{owner}/{name}")]
    public async Task<IActionResult> RepositoryInfo(string owner, string name)
    {
        var repo = await _github.GetRepositoryByNameFromDB(owner, name);
        if (repo == null)
        {
            return NotFound();
        }
        return Ok(repo);
    }


    [HttpGet("[action]/{owner}/{name}")]
    public async Task<IActionResult> LatestRepositoryRelease(string owner, string name)
    {
        var releases = await _github.GetLatestRepositoryReleaseFromDB(owner, name);
        if (releases == null)
        {
            return NotFound();
        }
        var lastTag = releases;
        return Ok(lastTag);
    }
}
