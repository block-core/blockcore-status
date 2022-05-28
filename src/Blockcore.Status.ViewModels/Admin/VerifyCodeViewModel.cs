namespace blockcore.status.ViewModels.Admin;

public class VerifyCodeViewModel
{
    [Required]
    public string Provider { get; set; }

    [Display(Name = "Security Code")]
    [Required(ErrorMessage = "Please Enter {0}")]
    public string Code { get; set; }

    public string ReturnUrl { get; set; }

    [Display(Name = "Remembering the current browser?")]
    public bool RememberBrowser { get; set; }

    [Display(Name = "Accreditation Storage?")]
    public bool RememberMe { get; set; }
}