using blockcore.status.Entities.Admin;

namespace blockcore.status.ViewModels.Admin;

public class UserCardItemViewModel
{
    public User User { set; get; }
    public bool ShowAdminParts { set; get; }
    public List<Role> Roles { get; set; }
    public UserCardItemActiveTab ActiveTab { get; set; }
}