using blockcore.status.Entities.Admin;

namespace blockcore.status.ViewModels.Admin.Emails;

public class RegisterEmailConfirmationViewModel : EmailsBase
{
    public User User { set; get; }
    public string EmailConfirmationToken { set; get; }
}