﻿using System.Security.Claims;
using BlockcoreStatus.DataLayer.Context;
using BlockcoreStatus.Entities.Admin;
using BlockcoreStatus.Services.Contracts.Admin;
using BlockcoreStatus.ViewModels.Admin;
using PersianUtils.Core;
using Microsoft.EntityFrameworkCore;

namespace BlockcoreStatus.Services.Admin;

public class SiteStatService : ISiteStatService
{
    private readonly IUnitOfWork _uow;
    private readonly IApplicationUserManager _userManager;
    private readonly DbSet<User> _users;

    public SiteStatService(
        IApplicationUserManager userManager,
        IUnitOfWork uow)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _users = uow.Set<User>();
    }

    public Task<List<User>> GetOnlineUsersListAsync(int numbersToTake, int minutesToTake)
    {
        var now = DateTime.UtcNow;
        var minutes = now.AddMinutes(-minutesToTake);
        return _users.AsNoTracking()
            .Where(user => user.LastVisitDateTime != null && user.LastVisitDateTime.Value <= now
                                                          && user.LastVisitDateTime.Value >= minutes)
            .OrderByDescending(user => user.LastVisitDateTime)
            .Take(numbersToTake)
            .ToListAsync();
    }

    public Task<List<User>> GetTodayBirthdayListAsync()
    {
        var now = DateTime.UtcNow;
        var day = now.Day;
        var month = now.Month;
        return _users.AsNoTracking()
            .Where(user => user.BirthDate != null && user.Online
                                                  && user.BirthDate.Value.Day == day
                                                  && user.BirthDate.Value.Month == month)
            .ToListAsync();
    }

    public async Task<AgeStatViewModel> GetUsersAverageAge()
    {
        var users = await _users.AsNoTracking()
            .Where(x => x.BirthDate != null && x.Online)
            .OrderBy(x => x.BirthDate)
            .ToListAsync();

        var count = users.Count;
        if (count == 0)
        {
            return new AgeStatViewModel();
        }

        var sum = users.Where(user => user.BirthDate != null).Sum(user => (int?)user.BirthDate.Value.GetAge()) ?? 0;

        return new AgeStatViewModel
        {
            AverageAge = sum / count,
            MaxAgeUser = users.First(),
            MinAgeUser = users.Last(),
            UsersCount = count
        };
    }

    public async Task UpdateUserLastVisitDateTimeAsync(ClaimsPrincipal claimsPrincipal)
    {
        var user = await _userManager.GetUserAsync(claimsPrincipal);
        user.LastVisitDateTime = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);
    }
}