namespace blockcore.status.ViewModels.Admin;

public class SearchUsersViewModel
{
    [Display(Name = "Phrase Search")]
    public string TextToFind { set; get; }

    [Display(Name = "Part of Email")]
    public bool IsPartOfEmail { set; get; }

    [Display(Name = "User Number")]
    public bool IsUserId { set; get; }

    [Display(Name = "Part of Name")]
    public bool IsPartOfName { set; get; }

    [Display(Name = "Part of Last Name")]
    public bool IsPartOfLastName { set; get; }

    [Display(Name = "Part of Username")]
    public bool IsPartOfUserName { set; get; }

    [Display(Name = "Part of accommodation")]
    public bool IsPartOfLocation { set; get; }

    [Display(Name = "Confirmed Email")]
    public bool HasEmailConfirmed { set; get; }

    [Display(Name = "Activities Only")]
    public bool UserIsActive { set; get; }

    [Display(Name = "Active and Disabled Users")]
    public bool ShowAllUsers { set; get; }

    [Display(Name = "Locked Account")]
    public bool UserIsLockedOut { set; get; }

    [Display(Name = "Two-Step Validation")]
    public bool HasTwoFactorEnabled { set; get; }

    [Display(Name = "Number of Rows Returned")]
    [Required(ErrorMessage = "Please Enter {0}")]
    [Range(1, 1000, ErrorMessage = "The number entered must be specified in the range 1 to 1000")]
    public int MaxNumberOfRows { set; get; }

    public PagedUsersListViewModel PagedUsersList { set; get; }

    public SearchUsersViewModel()
    {
        ShowAllUsers = true;
        MaxNumberOfRows = 7;
    }
}