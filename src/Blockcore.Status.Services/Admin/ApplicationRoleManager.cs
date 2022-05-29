﻿using BlockcoreStatus.DataLayer.Context;
using BlockcoreStatus.Entities.Admin;
using BlockcoreStatus.Services.Contracts.Admin;
using BlockcoreStatus.ViewModels.Admin;
using Common.Web.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlockcoreStatus.Services.Admin;

public class ApplicationRoleManager :
    RoleManager<Role>,
    IApplicationRoleManager
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IdentityErrorDescriber _errors;
    private readonly ILookupNormalizer _keyNormalizer;
    private readonly ILogger<ApplicationRoleManager> _logger;
    private readonly IEnumerable<IRoleValidator<Role>> _roleValidators;
    private readonly IApplicationRoleStore _store;
    private readonly IUnitOfWork _uow;
    private readonly DbSet<User> _users;

    public ApplicationRoleManager(
        IApplicationRoleStore store,
        IEnumerable<IRoleValidator<Role>> roleValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        ILogger<ApplicationRoleManager> logger,
        IHttpContextAccessor contextAccessor,
        IUnitOfWork uow) :
        base((RoleStore<Role, ApplicationDbContext, int, UserRole, RoleClaim>)store, roleValidators, keyNormalizer,
            errors, logger)
    {
        _store = store ?? throw new ArgumentNullException(nameof(store));
        _roleValidators = roleValidators ?? throw new ArgumentNullException(nameof(roleValidators));
        _keyNormalizer = keyNormalizer ?? throw new ArgumentNullException(nameof(keyNormalizer));
        _errors = errors ?? throw new ArgumentNullException(nameof(errors));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _users = _uow.Set<User>();
    }

    #region BaseClass

    #endregion

    #region CustomMethods

    public IList<Role> FindCurrentUserRoles()
    {
        var userId = GetCurrentUserId();
        return FindUserRoles(userId);
    }

    public IList<Role> FindUserRoles(int userId)
    {
        var userRolesQuery = from role in Roles
                             from user in role.Users
                             where user.UserId == userId
                             select role;

        return userRolesQuery.OrderBy(x => x.Name).ToList();
    }

    public Task<List<Role>> GetAllCustomRolesAsync()
    {
        return Roles.ToListAsync();
    }

    public IList<RoleAndUsersCountViewModel> GetAllCustomRolesAndUsersCountList()
    {
        return Roles.Select(role =>
            new RoleAndUsersCountViewModel
            {
                Role = role,
                UsersCount = role.Users.Count()
            }).ToList();
    }

    public async Task<PagedUsersListViewModel> GetPagedApplicationUsersInRoleListAsync(
        int roleId,
        int pageNumber, int recordsPerPage,
        string sortByField, SortOrder sortOrder,
        bool showAllUsers)
    {
        var skipRecords = pageNumber * recordsPerPage;

        var roleUserIdsQuery = from role in Roles
                               where role.Id == roleId
                               from user in role.Users
                               select user.UserId;
        var query = _users.Include(user => user.Roles)
            .Where(user => roleUserIdsQuery.Contains(user.Id))
            .AsNoTracking();

        if (!showAllUsers)
        {
            query = query.Where(x => x.IsActive);
        }

        switch (sortByField)
        {
            default:
                query = sortOrder == SortOrder.Descending
                    ? query.OrderByDescending(x => x.Id)
                    : query.OrderBy(x => x.Id);
                break;
        }

        return new PagedUsersListViewModel
        {
            Paging =
            {
                TotalItems = await query.CountAsync()
            },
            Users = await query.Skip(skipRecords).Take(recordsPerPage).ToListAsync(),
            Roles = await Roles.ToListAsync()
        };
    }

    public IList<User> GetApplicationUsersInRole(string roleName)
    {
        var roleUserIdsQuery = from role in Roles
                               where role.Name == roleName
                               from user in role.Users
                               select user.UserId;
        return _uow.Set<User>().Where(applicationUser => roleUserIdsQuery.Contains(applicationUser.Id))
            .ToList();
    }

    public IList<Role> GetRolesForCurrentUser()
    {
        var userId = GetCurrentUserId();
        return GetRolesForUser(userId);
    }

    public IList<Role> GetRolesForUser(int userId)
    {
        var roles = FindUserRoles(userId);
        if (roles == null || !roles.Any())
        {
            return new List<Role>();
        }

        return roles.ToList();
    }

    public IList<UserRole> GetUserRolesInRole(string roleName)
    {
        return Roles.Where(role => role.Name == roleName)
            .SelectMany(role => role.Users)
            .ToList();
    }

    public bool IsCurrentUserInRole(string roleName)
    {
        var userId = GetCurrentUserId();
        return IsUserInRole(userId, roleName);
    }

    public bool IsUserInRole(int userId, string roleName)
    {
        var userRolesQuery = from role in Roles
                             where role.Name == roleName
                             from user in role.Users
                             where user.UserId == userId
                             select role;
        var userRole = userRolesQuery.FirstOrDefault();
        return userRole != null;
    }

    public Task<Role> FindRoleIncludeRoleClaimsAsync(int roleId)
    {
        return Roles.Include(x => x.Claims).FirstOrDefaultAsync(x => x.Id == roleId);
    }

    public async Task<IdentityResult> AddOrUpdateRoleClaimsAsync(
        int roleId,
        string roleClaimType,
        IList<string> selectedRoleClaimValues)
    {
        var role = await FindRoleIncludeRoleClaimsAsync(roleId);
        if (role == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "RoleNotFound",
                Description = "Role Not Found"
            });
        }

        var currentRoleClaimValues = role.Claims.Where(roleClaim =>
                string.Equals(roleClaim.ClaimType, roleClaimType, StringComparison.Ordinal))
            .Select(roleClaim => roleClaim.ClaimValue)
            .ToList();

        selectedRoleClaimValues ??= new List<string>();

        var newClaimValuesToAdd = selectedRoleClaimValues.Except(currentRoleClaimValues).ToList();
        foreach (var claimValue in newClaimValuesToAdd)
        {
            role.Claims.Add(new RoleClaim
            {
                RoleId = role.Id,
                ClaimType = roleClaimType,
                ClaimValue = claimValue
            });
        }

        var removedClaimValues = currentRoleClaimValues.Except(selectedRoleClaimValues).ToList();
        foreach (var claimValue in removedClaimValues)
        {
            var roleClaim = role.Claims.SingleOrDefault(rc =>
                string.Equals(rc.ClaimValue, claimValue, StringComparison.Ordinal) &&
                string.Equals(rc.ClaimType, roleClaimType, StringComparison.Ordinal));
            if (roleClaim != null)
            {
                role.Claims.Remove(roleClaim);
            }
        }

        return await UpdateAsync(role);
    }

    private int GetCurrentUserId()
    {
        return _contextAccessor.HttpContext?.User.Identity?.GetUserId<int>() ?? 0;
    }

    #endregion
}