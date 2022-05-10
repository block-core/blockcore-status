using Microsoft.AspNetCore.Mvc;

namespace blockcore.status.ViewModels.Admin;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Please Enter {0}")]
    [Display(Name = "Username")]
    [Remote("ValidateUsername", "Register",
                 AdditionalFields = nameof(Email) + "," + ViewModelConstants.AntiForgeryToken, HttpMethod = "POST")]
    [RegularExpression("^ [a-zA-Z _] * $", ErrorMessage = "Please use only English letters")]
    public string Username { get; set; }

    [Display(Name = "FirstName")]
    [Required(ErrorMessage = "Please Enter {0}")]
    [StringLength(450)]

    public string FirstName { get; set; }

    [Display(Name = "LastName")]
    [Required(ErrorMessage = "Please Enter {0}")]
    [StringLength(450)]

    public string LastName { get; set; }

    [Required(ErrorMessage = "Please Enter {0}")]
    [Remote("ValidateUsername", "Register",
        AdditionalFields = nameof(Username) + "," + ViewModelConstants.AntiForgeryToken, HttpMethod = "POST")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Please Enter {0}")]
    [StringLength(100, ErrorMessage = "{0} must be at least {2} and at most {1} characters.", MinimumLength = 6)]
    [Remote("ValidatePassword", "Register",
                 AdditionalFields = nameof(Username) + "," + ViewModelConstants.AntiForgeryToken, HttpMethod = "POST")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Please Enter {0}")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare(nameof(Password), ErrorMessage = "Passwords entered do not match")]
    public string ConfirmPassword { get; set; }
}