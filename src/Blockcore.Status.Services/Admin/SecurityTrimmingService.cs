﻿using System.Security.Claims;
using BlockcoreStatus.Services.Contracts.Admin;
using Common.Web.Core;
using Microsoft.AspNetCore.Http;

namespace BlockcoreStatus.Services.Admin;

public class SecurityTrimmingService : ISecurityTrimmingService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMvcActionsDiscoveryService _mvcActionsDiscoveryService;

    public SecurityTrimmingService(
        IHttpContextAccessor httpContextAccessor,
        IMvcActionsDiscoveryService mvcActionsDiscoveryService)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _mvcActionsDiscoveryService = mvcActionsDiscoveryService ??
                                      throw new ArgumentNullException(nameof(mvcActionsDiscoveryService));
    }

    public bool CanCurrentUserAccess(string area, string controller, string action)
    {
        return _httpContextAccessor.HttpContext != null &&
               CanUserAccess(_httpContextAccessor.HttpContext.User, area, controller, action);
    }

    public bool CanUserAccess(ClaimsPrincipal user, string area, string controller, string action)
    {
        var currentClaimValue = $"{area}:{controller}:{action}";
        var securedControllerActions =
            _mvcActionsDiscoveryService.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);
        if (securedControllerActions.SelectMany(x => x.MvcActions)
            .All(x => !string.Equals(x.ActionId, currentClaimValue, StringComparison.Ordinal)))
        {
            throw new KeyNotFoundException(
                $"The `secured` area={area}/controller={controller}/action={action} with `ConstantPolicies.DynamicPermission` policy not found. Please check you have entered the area/controller/action names correctly and also it's decorated with the correct security policy.");
        }

        if (user?.Identity is null || !user.Identity.IsAuthenticated)
        {
            return false;
        }

        if (user.IsInRole(ConstantRoles.Admin))
        {
            // Admin users have access to all of the pages.
            return true;
        }

        // Check for dynamic permissions
        // A user gets its permissions claims from the `ApplicationClaimsPrincipalFactory` class automatically and it includes the role claims too.
        return user.HasClaim(claim =>
            string.Equals(claim.Type, ConstantPolicies.DynamicPermissionClaimType, StringComparison.Ordinal) &&
            string.Equals(claim.Value, currentClaimValue, StringComparison.Ordinal));
    }
}