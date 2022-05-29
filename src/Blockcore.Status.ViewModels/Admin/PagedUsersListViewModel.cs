using BlockcoreStatus.Entities.Admin;
using cloudscribe.Web.Pagination;

namespace BlockcoreStatus.ViewModels.Admin;

public class PagedUsersListViewModel
{
    public PagedUsersListViewModel()
    {
        Paging = new PaginationSettings();
    }

    public List<User> Users { get; set; }

    public List<Role> Roles { get; set; }

    public PaginationSettings Paging { get; set; }
}