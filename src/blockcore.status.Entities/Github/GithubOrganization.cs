using blockcore.status.Entities.AuditableEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace blockcore.status.Entities.Github;

public class GithubOrganization : IAuditableEntity
{
    [HiddenInput]
    public int Id { get; set; }

    [Display(Name = "Name")]
    [Required(ErrorMessage = "Please Enter {0}")]
    public string OrganizationName { get; set; }
    public DateTime? CreatedDateTime { get; set; }
    public virtual ICollection<GithubRepository> Repository { get; set; }
}