using BlockcoreStatus.Entities.Admin;

namespace BlockcoreStatus.ViewModels.Admin;

public class TodayBirthDaysViewModel
{
    public List<User> Users { set; get; }

    public AgeStatViewModel AgeStat { set; get; }
}