using blockcore.status.Entities.Admin;

namespace blockcore.status.ViewModels.Admin;

public class OnlineUsersViewModel
{
    public List<User> Users { set; get; }
    public int NumbersToTake { set; get; }
    public int MinutesToTake { set; get; }
    public bool ShowMoreItemsLink { set; get; }
}