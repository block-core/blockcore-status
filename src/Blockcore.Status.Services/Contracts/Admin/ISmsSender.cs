namespace BlockcoreStatus.Services.Contracts.Admin;

public interface ISmsSender
{
    #region BaseClass

    Task SendSmsAsync(string number, string message);

    #endregion

    #region CustomMethods

    #endregion
}