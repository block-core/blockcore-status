using blockcore.status.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace blockcore.status.Entities.Github;

public class GithubRepository : IAuditableEntity
{
    [Key]
    public int GithubRepositoryId { get; set; }
    public string Url { get; set; }
    public string HtmlUrl { get; set; }
    public string CloneUrl { get; set; }
    public string GitUrl { get; set; }
    public string SshUrl { get; set; }
    public string SvnUrl { get; set; }
    public string MirrorUrl { get; set; }
    public long Id { get; set; }
    public string NodeId { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public string Description { get; set; }
    public string Homepage { get; set; }
    public string Language { get; set; }
    public int ForksCount { get; set; }
    public int StargazersCount { get; set; }
    public int WatchersCount { get; set; }
    public string DefaultBranch { get; set; }
    public int OpenIssuesCount { get; set; }
    public DateTime? PushedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool HasIssues { get; set; }
    public bool HasWiki { get; set; }
    public bool HasDownloads { get; set; }
    public bool HasPages { get; set; }
    public long Size { get; set; }
    public bool Archived { get; set; }
    public bool IsSelect { get; set; }
    public DateTime LatestDataUpdate { get; set; }
    public int GithubOrganizationId { get; set; }
    public virtual GithubOrganization GithubOrganization { get; set; }
    public virtual ICollection<GithubRelease> GithubRelease { get; set; }

}