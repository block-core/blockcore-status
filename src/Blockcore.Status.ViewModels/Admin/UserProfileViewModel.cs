using Common.Web.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlockcoreStatus.ViewModels.Admin;

public class UserProfileViewModel
{
    public const string AllowedImages = ".png, .jpg, .jpeg, .gif";

    [Required(ErrorMessage = "(*)")]
    [Display(Name = "Username")]
    [Remote("ValidateUsername", "UserProfile",
        AdditionalFields = nameof(Email) + "," + ViewModelConstants.AntiForgeryToken + "," + nameof(Pid),
        HttpMethod = "POST")]
    public string UserName { get; set; }

    [Display(Name = "First Name")]
    [Required(ErrorMessage = "Please Enter {0}")]
    [StringLength(450)]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    [Required(ErrorMessage = "Please Enter {0}")]
    [StringLength(450)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Please Enter {0}")]
    [Remote("ValidateUsername", "UserProfile",
        AdditionalFields = nameof(UserName) + "," + ViewModelConstants.AntiForgeryToken + "," + nameof(Pid),
        HttpMethod = "POST")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Display(Name = "Image")]
    [StringLength(maximumLength: 1000, ErrorMessage = "Maximum image address length is 1000 characters.")]
    public string PhotoFileName { set; get; }

    [UploadFileExtensions(AllowedImages,
        ErrorMessage = "Please send only one image" + AllowedImages + ".")]
    [DataType(DataType.Upload)]
    public IFormFile Photo { get; set; }

    public int? DateOfBirthYear { set; get; }
    public int? DateOfBirthMonth { set; get; }
    public int? DateOfBirthDay { set; get; }

    [Display(Name = "Accommodation")]
    public string Location { set; get; }

    [Display(Name = "Public Email Display")]
    public bool IsEmailPublic { set; get; }

    [Display(Name = "Enable 2-Step Validation")]
    public bool TwoFactorEnabled { set; get; }

    public bool IsPasswordTooOld { set; get; }

    [HiddenInput]
    public string Pid { set; get; }

    [HiddenInput]
    public bool IsAdminEdit { set; get; }
}