using blockcore.status.Entities.Admin;

namespace blockcore.status.Services.Contracts.Admin;

public interface IUsedPasswordsService
{
    Task<bool> IsPreviouslyUsedPasswordAsync(User user, string newPassword);
    Task AddToUsedPasswordsListAsync(User user);
    Task<bool> IsLastUserPasswordTooOldAsync(int userId);
    Task<DateTime?> GetLastUserPasswordChangeDateAsync(int userId);
}