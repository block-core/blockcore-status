using BlockcoreStatus.Entities.Admin;

namespace BlockcoreStatus.ViewModels.Admin.Emails;

public class ChangePasswordNotificationViewModel : EmailsBase
{
    public User User { set; get; }
}