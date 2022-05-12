namespace blockcore.status.Services.Contracts.Admin;

public interface IEmailSender
{
    #region BaseClass

    Task SendEmailAsync(string email, string subject, string message);

    #endregion

    #region CustomMethods

    Task SendEmailAsync<T>(string email, string subject, string viewNameOrPath, T model);

    #endregion
}