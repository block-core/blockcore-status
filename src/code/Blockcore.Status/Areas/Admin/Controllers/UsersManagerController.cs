﻿using blockcore.status.Common.IdentityToolkit;
using blockcore.status.Entities.Admin;
using blockcore.status.Services.Contracts.Admin;
using blockcore.status.Services.Admin;
using blockcore.status.ViewModels.Admin;
using BreadCrumb.Core;
using Common.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blockcore.status.Areas.Admin.Controllers;

[Authorize(Roles = ConstantRoles.Admin), Area(AreaConstants.AdminArea),
 BreadCrumb(Title = "Users Manager", UseDefaultRouteUrl = true, Order = 0)]
public class UsersManagerController : Controller
{
    private const int DefaultPageSize = 7;

    private readonly IApplicationRoleManager _roleManager;
    private readonly IApplicationUserManager _userManager;

    public UsersManagerController(
        IApplicationUserManager userManager,
        IApplicationRoleManager roleManager)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    [AjaxOnly, HttpPost, ValidateAntiForgeryToken, ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ActivateUserEmailStat(int userId)
    {
        User thisUser = null;
        var result = await _userManager.UpdateUserAndSecurityStampAsync(
            userId, user =>
            {
                user.EmailConfirmed = true;
                thisUser = user;
            });
        if (!result.Succeeded)
        {
            return BadRequest(result.DumpErrors(true));
        }

        return await ReturnUserCardPartialView(thisUser);
    }

    [AjaxOnly, HttpPost, ValidateAntiForgeryToken, ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ChangeUserLockoutMode(int userId, bool activate)
    {
        User thisUser = null;
        var result = await _userManager.UpdateUserAndSecurityStampAsync(
            userId, user =>
            {
                user.LockoutEnabled = activate;
                thisUser = user;
            });
        if (!result.Succeeded)
        {
            return BadRequest(result.DumpErrors(true));
        }

        return await ReturnUserCardPartialView(thisUser);
    }

    [AjaxOnly, HttpPost, ValidateAntiForgeryToken, ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ChangeUserRoles(int userId, int[] roleIds)
    {
        User thisUser = null;
        var result = await _userManager.AddOrUpdateUserRolesAsync(
            userId, roleIds, user => thisUser = user);
        if (!result.Succeeded)
        {
            return BadRequest(result.DumpErrors(true));
        }

        return await ReturnUserCardPartialView(thisUser);
    }

    [AjaxOnly, HttpPost, ValidateAntiForgeryToken, ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ChangeUserStat(int userId, bool activate)
    {
        User thisUser = null;
        var result = await _userManager.UpdateUserAndSecurityStampAsync(
            userId, user =>
            {
                user.IsActive = activate;
                thisUser = user;
            });
        if (!result.Succeeded)
        {
            return BadRequest(result.DumpErrors(true));
        }

        return await ReturnUserCardPartialView(thisUser);
    }

    [AjaxOnly, HttpPost, ValidateAntiForgeryToken, ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ChangeUserTwoFactorAuthenticationStat(int userId, bool activate)
    {
        User thisUser = null;
        var result = await _userManager.UpdateUserAndSecurityStampAsync(
            userId, user =>
            {
                user.TwoFactorEnabled = activate;
                thisUser = user;
            });
        if (!result.Succeeded)
        {
            return BadRequest(result.DumpErrors(true));
        }

        return await ReturnUserCardPartialView(thisUser);
    }

    [AjaxOnly, HttpPost, ValidateAntiForgeryToken, ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> EndUserLockout(int userId)
    {
        User thisUser = null;
        var result = await _userManager.UpdateUserAndSecurityStampAsync(
            userId, user =>
            {
                user.LockoutEnd = null;
                thisUser = user;
            });
        if (!result.Succeeded)
        {
            return BadRequest(result.DumpErrors(true));
        }

        return await ReturnUserCardPartialView(thisUser);
    }

    [BreadCrumb(Title = "Index", Order = 1)]
    public async Task<IActionResult> Index(int? page = 1, string field = "Id", SortOrder order = SortOrder.Ascending)
    {
        var model = await _userManager.GetPagedUsersListAsync(
            page.Value - 1,
            DefaultPageSize,
            field,
            order,
            true);

        model.Paging.CurrentPage = page.Value;
        model.Paging.ItemsPerPage = DefaultPageSize;
        model.Paging.ShowFirstLast = true;

        if (HttpContext.Request.IsAjaxRequest())
        {
            return PartialView("_UsersList", model);
        }

        return View(model);
    }

    [AjaxOnly, HttpPost, ValidateAntiForgeryToken, ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> SearchUsers(SearchUsersViewModel model)
    {
        if (model is null)
        {
            return BadRequest();
        }

        var pagedUsersList = await _userManager.GetPagedUsersListAsync(
            model,
            0);

        pagedUsersList.Paging.CurrentPage = 1;
        pagedUsersList.Paging.ItemsPerPage = model.MaxNumberOfRows;
        pagedUsersList.Paging.ShowFirstLast = true;

        model.PagedUsersList = pagedUsersList;
        return PartialView("_SearchUsers", model);
    }

    private async Task<IActionResult> ReturnUserCardPartialView(User thisUser)
    {
        var roles = await _roleManager.GetAllCustomRolesAsync();
        return PartialView("~/Areas/Admin/Views/UserCard/_UserCardItem.cshtml",
            new UserCardItemViewModel
            {
                User = thisUser,
                ShowAdminParts = true,
                Roles = roles,
                ActiveTab = UserCardItemActiveTab.UserAdmin
            });
    }
}