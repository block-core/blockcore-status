namespace BlockcoreStatus.ViewModels.Admin.Settings;

public class DataProtectionOptions
{
    public TimeSpan DataProtectionKeyLifetime { get; set; }
    public string ApplicationName { get; set; }
}