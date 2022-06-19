using BlockcoreStatus.ViewModels.Indexers;
using cloudscribe.Web.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockcoreStatus.ViewModels.Indexers;
public class PagedIndexersViewModel
{
    public PagedIndexersViewModel()
    {
        Paging = new PaginationSettings();
    }
    public List<IndexersViewModel> Indexers { get; set; }
    public PaginationSettings Paging { get; set; }
}
