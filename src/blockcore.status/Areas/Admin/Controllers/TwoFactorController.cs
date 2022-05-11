using blockcore.status.Services.Contracts.Admin;
using blockcore.status.ViewModels.Admin;
using blockcore.status.ViewModels.Admin.Emails;
using blockcore.status.ViewModels.Admin.Settings;
using BreadCrumb.Core;
using PersianUtils.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace blockcore.status.Areas.Identity.Controllers;

[Authorize, Area(AreaConstants.AdminArea),
 BreadCrumb(Title = "Two Factor", UseDefaultRouteUrl = true, Order = 0)]
public class TwoFactorController : Controller
{
    private readonly IEmailSender _emailSender;
    private readonly IApplicationSignInManager _signInManager;
    private readonly IOptionsSnapshot<SiteSettings> _siteOptions;
    private readonly IApplicationUserManager _userManager;

    public TwoFactorController(
        IApplicationUserManager userManager,
        IApplicationSignInManager signInManager,
        IEmailSender emailSender,
        IOptionsSnapshot<SiteSettings> siteOptions)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        _siteOptions = siteOptions ?? throw new ArgumentNullException(nameof(siteOptions));
    }

    [AllowAnonymous, BreadCrumb(Title = "Send Code", Order = 1)]
    public async Task<IActionResult> SendCode(string returnUrl = null, bool rememberMe = false)
    {
        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        if (user == null)
        {
            return View("NotFound");
        }

        var tokenProvider = "Email";
        var code = await _userManager.GenerateTwoFactorTokenAsync(user, tokenProvider);
        if (string.IsNullOrWhiteSpace(code))
        {
            return View("Error");
        }

        await _emailSender.SendEmailAsync(
            user.Email,
            "New Two-Factor validation code",
            "~/Areas/Admin/Views/EmailTemplates/_TwoFactorSendCode.cshtml",
            new TwoFactorSendCodeViewModel
            {
                Token = code,
                EmailSignature = _siteOptions.Value.Smtp.FromName,
                MessageDateTime = DateTime.UtcNow.ToLongPersianDateTimeString()
            });

        return RedirectToAction(
            nameof(VerifyCode),
            new
            {
                Provider = tokenProvider,
                ReturnUrl = returnUrl,
                RememberMe = rememberMe
            });
    }

    [AllowAnonymous, BreadCrumb(Title = "Verify Code", Order = 1)]
    public async Task<IActionResult> VerifyCode(string provider, bool rememberMe, string returnUrl = null)
    {
        // Require that the user has already logged in via username/password or external login
        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        if (user == null)
        {
            return View("NotFound");
        }

        return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
    }

    [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
    public async Task<IActionResult> VerifyCode(VerifyCodeViewModel model)
    {
        if (model is null)
        {
            return View("Error");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // The following code protects for brute force attacks against the two factor codes.
        // If a user enters incorrect codes for a specified amount of time then the user account
        // will be locked out for a specified amount of time.
        var result = await _signInManager.TwoFactorSignInAsync(
            model.Provider,
            model.Code,
            model.RememberMe,
            model.RememberBrowser);

        if (result.Succeeded)
        {
            var returnUrl = model.ReturnUrl;
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        if (result.IsLockedOut)
        {
            return View("Lockout");
        }

        ModelState.AddModelError(string.Empty, "The entered code is invalid.");
        return View(model);
    }
}