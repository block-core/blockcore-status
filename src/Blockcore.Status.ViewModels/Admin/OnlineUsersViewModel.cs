using BlockcoreStatus.Entities.Admin;

namespace BlockcoreStatus.ViewModels.Admin;

public class OnlineUsersViewModel
{
    public List<User> Users { set; get; }
    public int NumbersToTake { set; get; }
    public int MinutesToTake { set; get; }
    public bool ShowMoreItemsLink { set; get; }
}