namespace BlockcoreStatus.ViewModels.Admin.Emails;

public class PasswordResetViewModel : EmailsBase
{
    public int UserId { set; get; }
    public string Token { set; get; }
}