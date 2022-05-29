using blockcore.status.Entities.Admin;
using System.Security.Claims;
using blockcore.status.ViewModels.Admin;

namespace blockcore.status.Services.Contracts.Admin;

public interface ISiteStatService
{
    Task<List<User>> GetOnlineUsersListAsync(int numbersToTake, int minutesToTake);

    Task<List<User>> GetTodayBirthdayListAsync();

    Task UpdateUserLastVisitDateTimeAsync(ClaimsPrincipal claimsPrincipal);

    Task<AgeStatViewModel> GetUsersAverageAge();
}