using blockcore.status.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace blockcore.status.Entities.Admin;

public class UserRole : IdentityUserRole<int>, IAuditableEntity
{
    public virtual User User { get; set; }

    public virtual Role Role { get; set; }
}