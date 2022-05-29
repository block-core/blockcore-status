using BlockcoreStatus.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace BlockcoreStatus.Entities.Admin;

public class RoleClaim : IdentityRoleClaim<int>, IAuditableEntity
{
    public virtual Role Role { get; set; }
}