using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockcoreStatus.Entities.Github;
public class GithubRelease
{
    [Key]
    public int GithubReleaseId { get; set; }
    public string Url { get; set; }
    public string AssetsUrl { get; set; }
    public string UploadUrl { get; set; }
    public string HtmlUrl { get; set; }
    public int Id { get; set; }
    public string NodeId { get; set; }
    public string TagName { get; set; }
    public string TargetCommitish { get; set; }
    public string Name { get; set; }
    public bool Draft { get; set; }
    public bool Prerelease { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? PublishedAt { get; set; }
    public string TarballUrl { get; set; }
    public string ZipballUrl { get; set; }
    public string Body { get; set; }
    public DateTimeOffset LatestDataUpdate { get; set; }

    public int GithubRepositoryId { get; set; }
    public virtual GithubRepository GithubRepository { get; set; }

}
