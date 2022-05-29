namespace BlockcoreStatus.ViewModels.Admin.Emails;

public class TwoFactorSendCodeViewModel : EmailsBase
{
    public string Token { set; get; }
}