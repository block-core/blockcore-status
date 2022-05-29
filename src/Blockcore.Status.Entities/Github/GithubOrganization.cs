using BlockcoreStatus.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlockcoreStatus.Entities.Github;

public class GithubOrganization : IAuditableEntity
{
    [Key]
    public int GithubOrganizationId { get; set; }
    public string Login { get; set; }
    public int Id { get; set; }
    public string Url { get; set; }
    public string ReposUrl { get; set; }
    public string EventsUrl { get; set; }
    public string HooksUrl { get; set; }
    public string IssuesUrl { get; set; }
    public string MembersUrl { get; set; }
    public string PublicMembersUrl { get; set; }
    public string AvatarUrl { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public string Company { get; set; }
    public string Blog { get; set; }
    public string Location { get; set; }
    public string Email { get; set; }
    public bool IsVerified { get; set; }
    public bool HasOrganizationProjects { get; set; }
    public bool HasRepositoryProjects { get; set; }
    public int PublicRepos { get; set; }
    public int PublicGists { get; set; }
    public int Followers { get; set; }
    public int Following { get; set; }
    public string HtmlUrl { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string Type { get; set; }
    public DateTimeOffset LatestDataUpdate { get; set; }

    public virtual ICollection<GithubRepository> GithubRepositories { get; set; }
}