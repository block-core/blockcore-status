using blockcore.status.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace blockcore.status.Entities.Admin;

public class RoleClaim : IdentityRoleClaim<int>, IAuditableEntity
{
    public virtual Role Role { get; set; }
}