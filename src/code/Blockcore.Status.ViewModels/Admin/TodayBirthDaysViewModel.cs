using blockcore.status.Entities.Admin;

namespace blockcore.status.ViewModels.Admin;

public class TodayBirthDaysViewModel
{
    public List<User> Users { set; get; }

    public AgeStatViewModel AgeStat { set; get; }
}