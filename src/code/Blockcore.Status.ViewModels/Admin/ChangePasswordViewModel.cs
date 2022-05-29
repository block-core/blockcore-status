using Microsoft.AspNetCore.Mvc;

namespace blockcore.status.ViewModels.Admin;

public class ChangePasswordViewModel
{
    [Required(ErrorMessage = "Please Enter {0}")]
    [DataType(DataType.Password)]
    [Display(Name = "Old Password")]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = "Please Enter {0}")]
    [StringLength(100, ErrorMessage = "{0} must be at least {2} and at most {1} characters.", MinimumLength = 6)]
    [Remote("ValidatePassword", "ChangePassword",
AdditionalFields = ViewModelConstants.AntiForgeryToken, HttpMethod = "POST")]
    [DataType(DataType.Password)]
    [Display(Name = "New Password")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Please Enter {0}")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare(nameof(NewPassword), ErrorMessage = "Passwords entered do not match")]
    public string ConfirmPassword { get; set; }

    public DateTime? LastUserPasswordChangeDate { get; set; }
}