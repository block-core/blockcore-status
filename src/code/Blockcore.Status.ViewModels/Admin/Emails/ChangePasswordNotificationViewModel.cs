using blockcore.status.Entities.Admin;

namespace blockcore.status.ViewModels.Admin.Emails;

public class ChangePasswordNotificationViewModel : EmailsBase
{
    public User User { set; get; }
}