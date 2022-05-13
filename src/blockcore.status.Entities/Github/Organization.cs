using blockcore.status.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace blockcore.status.Entities.Admin;

public class Organization : IAuditableEntity
{
    public int Id { get; set; }
    public string OrganizationName { get; set; }
    public DateTime? CreatedDateTime { get; set; }

}