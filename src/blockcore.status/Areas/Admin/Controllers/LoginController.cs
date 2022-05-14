using blockcore.status.Services.Contracts.Admin;
using blockcore.status.ViewModels.Admin;
using blockcore.status.ViewModels.Admin.Settings;
using BreadCrumb.Core;
using Common.Web.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace blockcore.status.Areas.Admin.Controllers;

[Area(AreaConstants.AdminArea), AllowAnonymous,
 BreadCrumb(Title = "Login", UseDefaultRouteUrl = true, Order = 0)]
public class LoginController : Controller
{
    private readonly IApplicationSignInManager _signInManager;
    private readonly IOptionsSnapshot<SiteSettings> _siteOptions;
    private readonly IApplicationUserManager _userManager;

    public LoginController(
        IApplicationSignInManager signInManager,
        IApplicationUserManager userManager,
        IOptionsSnapshot<SiteSettings> siteOptions)
    {
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _siteOptions = siteOptions ?? throw new ArgumentNullException(nameof(siteOptions));
    }

    [BreadCrumb(Title = "Index", Order = 1), NoBrowserCache]
    public IActionResult Index(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    public async Task<IActionResult> Index(LoginViewModel model, string returnUrl = null)
    {
        if (model is null)
        {
            return View("Error");
        }

        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "The username or password entered are invalid.");
                return View(model);
            }

            if (!user.IsActive)
            {
                ModelState.AddModelError(string.Empty, "Your account has been deactivated.");
                return View(model);
            }

            if (_siteOptions.Value.EnableEmailConfirmation &&
                !await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("", "Please refer to your email and confirm your email!");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                true);
            if (result.Succeeded)
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if (result.RequiresTwoFactor)
            {
                return RedirectToAction(
                    nameof(TwoFactorController.SendCode),
                    "TwoFactor",
                    new { ReturnUrl = returnUrl, model.RememberMe });
            }

            if (result.IsLockedOut)
            {
                return View("~/Areas/Admin/Views/TwoFactor/Lockout.cshtml");
            }

            if (result.IsNotAllowed)
            {
                ModelState.AddModelError(string.Empty, "No Login access.");
                return View(model);
            }

            ModelState.AddModelError(string.Empty, "The username or password entered are invalid.");
            return View(model);
        }

        // If we got this far, something failed, redisplay form
        return View(model);
    }

    public async Task<IActionResult> LogOff()
    {
        var user = User.Identity is { IsAuthenticated: true }
            ? await _userManager.FindByNameAsync(User.Identity.Name)
            : null;
        await _signInManager.SignOutAsync();
        if (user != null)
        {
            await _userManager.UpdateSecurityStampAsync(user);
        }

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
}