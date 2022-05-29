﻿using BlockcoreStatus.Entities.Admin;
using BlockcoreStatus.Services.Contracts.Admin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BlockcoreStatus.Services.Admin;

public class ApplicationSignInManager :
    SignInManager<User>,
    IApplicationSignInManager
{
    private readonly IUserClaimsPrincipalFactory<User> _claimsFactory;
    private readonly IUserConfirmation<User> _confirmation;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ILogger<ApplicationSignInManager> _logger;
    private readonly IOptions<IdentityOptions> _optionsAccessor;
    private readonly IAuthenticationSchemeProvider _schemes;
    private readonly IApplicationUserManager _userManager;

    public ApplicationSignInManager(
        IApplicationUserManager userManager,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<User> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<ApplicationSignInManager> logger,
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<User> confirmation)
        : base((UserManager<User>)userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes,
            confirmation)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        _claimsFactory = claimsFactory ?? throw new ArgumentNullException(nameof(claimsFactory));
        _optionsAccessor = optionsAccessor ?? throw new ArgumentNullException(nameof(optionsAccessor));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _schemes = schemes ?? throw new ArgumentNullException(nameof(schemes));
        _confirmation = confirmation;
    }

    #region BaseClass

    Task<bool> IApplicationSignInManager.IsLockedOut(User user)
    {
        return base.IsLockedOut(user);
    }

    Task<SignInResult> IApplicationSignInManager.LockedOut(User user)
    {
        return base.LockedOut(user);
    }

    Task<SignInResult> IApplicationSignInManager.PreSignInCheck(User user)
    {
        return base.PreSignInCheck(user);
    }

    Task IApplicationSignInManager.ResetLockout(User user)
    {
        return base.ResetLockout(user);
    }

    Task<SignInResult> IApplicationSignInManager.SignInOrTwoFactorAsync(User user, bool isPersistent,
        string loginProvider, bool bypassTwoFactor)
    {
        return base.SignInOrTwoFactorAsync(user, isPersistent, loginProvider, bypassTwoFactor);
    }

    #endregion

    #region CustomMethods

    public bool IsCurrentUserSignedIn()
    {
        return IsSignedIn(_contextAccessor.HttpContext?.User);
    }

    public Task<User> ValidateCurrentUserSecurityStampAsync()
    {
        return ValidateSecurityStampAsync(_contextAccessor.HttpContext?.User);
    }

    #endregion
}