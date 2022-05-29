using Microsoft.AspNetCore.Mvc;

namespace blockcore.status.ViewModels.Admin;

public class RoleViewModel
{
    [HiddenInput]
    public string Id { set; get; }

    [Required(ErrorMessage = "Please Enter {0}")]
    [Display(Name = "Role Name")]
    public string Name { set; get; }
}