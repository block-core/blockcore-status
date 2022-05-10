namespace blockcore.status.ViewModels.Admin;

public class LoginViewModel
{
    [Required(ErrorMessage = "Please enter {0}.")]
    [Display(Name = "Username")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Please enter {0}.")]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }
}