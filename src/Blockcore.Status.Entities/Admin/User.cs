﻿using BlockcoreStatus.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;

namespace BlockcoreStatus.Entities.Admin;

public class User : IdentityUser<int>, IAuditableEntity
{
    public User()
    {
        UserUsedPasswords = new HashSet<UserUsedPassword>();
        UserTokens = new HashSet<UserToken>();
    }

    [StringLength(450)] public string FirstName { get; set; }

    [StringLength(450)] public string LastName { get; set; }

    [NotMapped]
    public string DisplayName
    {
        get
        {
            var displayName = $"{FirstName} {LastName}";
            return string.IsNullOrWhiteSpace(displayName) ? UserName : displayName;
        }
    }

    [StringLength(450)] public string PhotoFileName { get; set; }

    public DateTime? BirthDate { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? LastVisitDateTime { get; set; }

    public bool IsEmailPublic { get; set; }

    public string Location { set; get; }

    public bool Online { get; set; } = true;

    public virtual ICollection<UserUsedPassword> UserUsedPasswords { get; set; }

    public virtual ICollection<UserToken> UserTokens { get; set; }

    public virtual ICollection<UserRole> Roles { get; set; }

    public virtual ICollection<UserLogin> Logins { get; set; }

    public virtual ICollection<UserClaim> Claims { get; set; }
}