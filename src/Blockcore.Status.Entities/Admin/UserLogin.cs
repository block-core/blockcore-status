using BlockcoreStatus.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace BlockcoreStatus.Entities.Admin;

public class UserLogin : IdentityUserLogin<int>, IAuditableEntity
{
    public virtual User User { get; set; }
}