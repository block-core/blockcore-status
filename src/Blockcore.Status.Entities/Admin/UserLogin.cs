using blockcore.status.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace blockcore.status.Entities.Admin;

public class UserLogin : IdentityUserLogin<int>, IAuditableEntity
{
    public virtual User User { get; set; }
}