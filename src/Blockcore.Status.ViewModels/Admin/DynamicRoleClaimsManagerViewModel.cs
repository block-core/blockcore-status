using BlockcoreStatus.Entities.Admin;
using Common.Web.Core;

namespace BlockcoreStatus.ViewModels.Admin;

public class DynamicRoleClaimsManagerViewModel
{
    public string[] ActionIds { set; get; }

    public int RoleId { set; get; }

    public Role RoleIncludeRoleClaims { set; get; }

    public ICollection<MvcControllerViewModel> SecuredControllerActions { set; get; }
}