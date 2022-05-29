﻿using System.Security.Claims;
using blockcore.status.DataLayer.Context;
using blockcore.status.Entities.Admin;
using blockcore.status.Services.Contracts.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace blockcore.status.Services.Admin;

public class ApplicationUserStore :
    UserStore<User, Role, ApplicationDbContext, int, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>,
    IApplicationUserStore
{
    private readonly IdentityErrorDescriber _describer;
    private readonly IUnitOfWork _uow;

    public ApplicationUserStore(
        IUnitOfWork uow,
        IdentityErrorDescriber describer)
        : base((ApplicationDbContext)uow, describer)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _describer = describer ?? throw new ArgumentNullException(nameof(describer));
    }

    #region BaseClass

    protected override UserClaim CreateUserClaim(User user, Claim claim)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var userClaim = new UserClaim { UserId = user.Id };
        userClaim.InitializeFromClaim(claim);
        return userClaim;
    }

    protected override UserLogin CreateUserLogin(User user, UserLoginInfo login)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (login == null)
        {
            throw new ArgumentNullException(nameof(login));
        }

        return new UserLogin
        {
            UserId = user.Id,
            ProviderKey = login.ProviderKey,
            LoginProvider = login.LoginProvider,
            ProviderDisplayName = login.ProviderDisplayName
        };
    }

    protected override UserRole CreateUserRole(User user, Role role)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        if (role == null)
        {
            throw new ArgumentNullException(nameof(role));
        }

        return new UserRole
        {
            UserId = user.Id,
            RoleId = role.Id
        };
    }

    protected override UserToken CreateUserToken(User user, string loginProvider, string name, string value)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        return new UserToken
        {
            UserId = user.Id,
            LoginProvider = loginProvider,
            Name = name,
            Value = value
        };
    }

    Task IApplicationUserStore.AddUserTokenAsync(UserToken token)
    {
        return base.AddUserTokenAsync(token);
    }

    Task<Role> IApplicationUserStore.FindRoleAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        return base.FindRoleAsync(normalizedRoleName, cancellationToken);
    }

    Task<UserToken> IApplicationUserStore.FindTokenAsync(User user, string loginProvider, string name,
        CancellationToken cancellationToken)
    {
        return base.FindTokenAsync(user, loginProvider, name, cancellationToken);
    }

    Task<User> IApplicationUserStore.FindUserAsync(int userId, CancellationToken cancellationToken)
    {
        return base.FindUserAsync(userId, cancellationToken);
    }

    Task<UserLogin> IApplicationUserStore.FindUserLoginAsync(int userId, string loginProvider, string providerKey,
        CancellationToken cancellationToken)
    {
        return base.FindUserLoginAsync(userId, loginProvider, providerKey, cancellationToken);
    }

    Task<UserLogin> IApplicationUserStore.FindUserLoginAsync(string loginProvider, string providerKey,
        CancellationToken cancellationToken)
    {
        return base.FindUserLoginAsync(loginProvider, providerKey, cancellationToken);
    }

    Task<UserRole> IApplicationUserStore.FindUserRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
    {
        return base.FindUserRoleAsync(userId, roleId, cancellationToken);
    }

    Task IApplicationUserStore.RemoveUserTokenAsync(UserToken token)
    {
        return base.RemoveUserTokenAsync(token);
    }

    #endregion

    #region CustomMethods

    // Add custom methods here

    #endregion
}