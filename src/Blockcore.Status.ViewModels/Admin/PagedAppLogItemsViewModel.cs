using BlockcoreStatus.Entities.Admin;
using cloudscribe.Web.Pagination;

namespace BlockcoreStatus.ViewModels.Admin;

public class PagedAppLogItemsViewModel
{
    public PagedAppLogItemsViewModel()
    {
        Paging = new PaginationSettings();
    }

    public string LogLevel { get; set; } = string.Empty;

    public List<AppLogItem> AppLogItems { get; set; }

    public PaginationSettings Paging { get; set; }
}