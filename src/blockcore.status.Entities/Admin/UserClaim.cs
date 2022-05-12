using blockcore.status.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace blockcore.status.Entities.Admin;

public class UserClaim : IdentityUserClaim<int>, IAuditableEntity
{
    public virtual User User { get; set; }
}