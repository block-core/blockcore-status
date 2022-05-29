using BlockcoreStatus.Entities.Admin;

namespace BlockcoreStatus.ViewModels.Admin.Emails;

public class UserProfileUpdateNotificationViewModel : EmailsBase
{
    public User User { set; get; }
}