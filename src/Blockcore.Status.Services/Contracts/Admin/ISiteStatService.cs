using BlockcoreStatus.Entities.Admin;
using System.Security.Claims;
using BlockcoreStatus.ViewModels.Admin;

namespace BlockcoreStatus.Services.Contracts.Admin;

public interface ISiteStatService
{
    Task<List<User>> GetOnlineUsersListAsync(int numbersToTake, int minutesToTake);

    Task<List<User>> GetTodayBirthdayListAsync();

    Task UpdateUserLastVisitDateTimeAsync(ClaimsPrincipal claimsPrincipal);

    Task<AgeStatViewModel> GetUsersAverageAge();
}