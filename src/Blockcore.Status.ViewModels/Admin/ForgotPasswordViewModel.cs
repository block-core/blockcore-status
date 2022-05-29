namespace blockcore.status.ViewModels.Admin;

public class ForgotPasswordViewModel
{
    [Required(ErrorMessage = "Please Enter {0}")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [Display(Name = "Email")]
    public string Email { get; set; }
}