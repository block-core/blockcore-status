using blockcore.status.Common.IdentityToolkit;
using blockcore.status.Entities.Admin;
using blockcore.status.Services.Contracts.Admin;
using blockcore.status.ViewModels.Admin;
using blockcore.status.ViewModels.Admin.Emails;
using blockcore.status.ViewModels.Admin.Settings;
using BreadCrumb.Core;
using Captcha.Core;
using Common.Web.Core;
using PersianUtils.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Language = Captcha.Core.Language;

namespace blockcore.status.Areas.Identity.Controllers;

[Area(AreaConstants.AdminArea), AllowAnonymous,
 BreadCrumb(Title = "Forgot Password", UseDefaultRouteUrl = true, Order = 0)]
public class ForgotPasswordController : Controller
{
    private readonly IEmailSender _emailSender;
    private readonly IPasswordValidator<User> _passwordValidator;
    private readonly IOptionsSnapshot<SiteSettings> _siteOptions;
    private readonly IApplicationUserManager _userManager;

    public ForgotPasswordController(
        IApplicationUserManager userManager,
        IPasswordValidator<User> passwordValidator,
        IEmailSender emailSender,
        IOptionsSnapshot<SiteSettings> siteOptions)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _passwordValidator = passwordValidator ?? throw new ArgumentNullException(nameof(passwordValidator));
        _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        _siteOptions = siteOptions ?? throw new ArgumentNullException(nameof(siteOptions));
    }

    [BreadCrumb(Title = "Forgot Password Confirmation", Order = 1)]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    [BreadCrumb(Title = "Index", Order = 1)]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost, ValidateAntiForgeryToken, ValidateCaptcha(CaptchaGeneratorLanguage = Language.English,
         CaptchaGeneratorDisplayMode = DisplayMode.ShowDigits, ErrorMessage = "Please enter the security code correctly")]
    public async Task<IActionResult> Index(ForgotPasswordViewModel model)
    {
        if (model is null)
        {
            return View("Error");
        }

        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            {
                return View("Error");
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _emailSender.SendEmailAsync(
                    model.Email,
                    "Reset Password",
                    "~/Areas/Admin/Views/EmailTemplates/_PasswordReset.cshtml",
                    new PasswordResetViewModel
                    {
                        UserId = user.Id,
                        Token = code,
                        EmailSignature = _siteOptions.Value.Smtp.FromName,
                        MessageDateTime = DateTime.UtcNow.ToShortTimeString()
                    })
                ;

            return View("ForgotPasswordConfirmation");
        }

        return View(model);
    }

    /// <summary>
    ///     For [Remote] validation
    /// </summary>
    [AjaxOnly, HttpPost, ValidateAntiForgeryToken, ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> ValidatePassword(string password, string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Json("The entered email is not valid.");
        }

        var result = await _passwordValidator.ValidateAsync(
            (UserManager<User>)_userManager, user, password);
        return Json(result.Succeeded ? "true" : result.DumpErrors(true));
    }

    [BreadCrumb(Title = "Reset Password", Order = 1)]
    public IActionResult ResetPassword(string code = null)
    {
        return code == null ? View("Error") : View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (model is null)
        {
            return View("Error");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            // Don't reveal that the user does not exist
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View();
    }

    [BreadCrumb(Title = "Reset Password Confirmation", Order = 1)]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }
}