using blockcore.status.ViewModels.Admin;

namespace blockcore.status.Services.Contracts.Admin;

public interface IAppLogItemsService
{
    Task DeleteAllAsync(string logLevel = "");
    Task DeleteAsync(int logItemId);
    Task DeleteOlderThanAsync(DateTime cutoffDateUtc, string logLevel = "");
    Task<int> GetCountAsync(string logLevel = "");
    Task<PagedAppLogItemsViewModel> GetPagedAppLogItemsAsync(int pageNumber, int pageSize, SortOrder sortOrder, string logLevel = "");
}