using Microsoft.AspNetCore.Mvc;

namespace BlockcoreStatus.ViewModels.Admin;

public class ResetPasswordViewModel
{
    [Required(ErrorMessage = "Please Enter {0}")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Please Enter {0}")]
    [StringLength(100, ErrorMessage = "{0} must be at least {2} and at most {1} characters.", MinimumLength = 6)]
    [Remote("ValidatePassword", "ForgotPassword",
        AdditionalFields = nameof(Email) + "," + ViewModelConstants.AntiForgeryToken, HttpMethod = "POST")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Please Enter {0}")]
    [DataType(DataType.Password)]
    [Display(Name = "Repeat Password")]
    [Compare(nameof(Password), ErrorMessage = "Passwords entered do not match")]
    public string ConfirmPassword { get; set; }

    public string Code { get; set; }
}