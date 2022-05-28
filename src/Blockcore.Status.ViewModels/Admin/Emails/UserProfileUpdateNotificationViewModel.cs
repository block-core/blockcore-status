using blockcore.status.Entities.Admin;

namespace blockcore.status.ViewModels.Admin.Emails;

public class UserProfileUpdateNotificationViewModel : EmailsBase
{
    public User User { set; get; }
}