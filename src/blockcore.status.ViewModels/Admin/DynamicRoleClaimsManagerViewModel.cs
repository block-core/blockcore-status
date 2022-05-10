using blockcore.status.Entities.Admin;
using Common.Web.Core;

namespace blockcore.status.ViewModels.Admin;

public class DynamicRoleClaimsManagerViewModel
{
    public string[] ActionIds { set; get; }

    public int RoleId { set; get; }

    public Role RoleIncludeRoleClaims { set; get; }

    public ICollection<MvcControllerViewModel> SecuredControllerActions { set; get; }
}