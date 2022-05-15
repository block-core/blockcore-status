using blockcore.status.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace blockcore.status.Entities.Github;

public class GithubRepository : IAuditableEntity
{
    public int Id { get; set; }
    public string RepositoryName { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public int OrganizationId { get; set; }
    public virtual GithubOrganization Organization { get; set; }

}