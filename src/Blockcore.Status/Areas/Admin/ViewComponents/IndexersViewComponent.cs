using BlockcoreStatus.Services.Contracts;
using BlockcoreStatus.Services.Contracts.Admin;
using BlockcoreStatus.ViewModels.Admin;
using BlockcoreStatus.ViewModels.Github;
using Microsoft.AspNetCore.Mvc;

namespace BlockcoreStatus.Areas.Admin.ViewComponents;

public class IndexersViewComponent : ViewComponent
{
    private readonly IBlockcoreIndexersService _indexer;

    public IndexersViewComponent(IBlockcoreIndexersService indexer)
    {
        _indexer = indexer;
    }


    public async Task<IViewComponentResult> InvokeAsync( )
    {
        var indexers = await _indexer.GetIndexers();

        return View("~/Areas/Admin/Views/Shared/Components/Indexers/Default.cshtml", indexers);
    }

}