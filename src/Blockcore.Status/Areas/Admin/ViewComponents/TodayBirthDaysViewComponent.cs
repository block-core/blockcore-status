﻿using BlockcoreStatus.Services.Contracts.Admin;
using BlockcoreStatus.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace BlockcoreStatus.Areas.Admin.ViewComponents;

public class TodayBirthDaysViewComponent : ViewComponent
{
    private readonly ISiteStatService _siteStatService;

    public TodayBirthDaysViewComponent(ISiteStatService siteStatService)
    {
        _siteStatService = siteStatService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var usersList = await _siteStatService.GetTodayBirthdayListAsync();
        var usersAverageAge = await _siteStatService.GetUsersAverageAge();

        return View("~/Areas/Admin/Views/Shared/Components/TodayBirthDays/Default.cshtml",
            new TodayBirthDaysViewModel
            {
                Users = usersList,
                AgeStat = usersAverageAge
            });
    }
}