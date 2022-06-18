using BlockcoreStatus.DataLayer.Context;
using BlockcoreStatus.Entities;
using BlockcoreStatus.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Linq;
using BlockcoreStatus.ViewModels.Indexers;
using BlockcoreStatus.Common.WebToolkit;

namespace BlockcoreStatus.Services;

public class EfBlockcoreIndexersService : IBlockcoreIndexersService
{
    private readonly IUnitOfWork _uow;
    private readonly IBlockcoreChainsService _blockcoreChains;
    private static readonly Regex _regex = new Regex(@"(http|ftp|https):\/\/([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?");

    public EfBlockcoreIndexersService(IUnitOfWork uow, IBlockcoreChainsService blockcoreChains)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _blockcoreChains = blockcoreChains;
    }

    public async Task<List<IndexersViewModel>> GetAllIndexers()
    {
        var urllist = new List<string>();
        using (HttpClient _httpClient = new HttpClient())
        {
            string txt = await _httpClient.GetStringAsync("https://raw.githubusercontent.com/block-core/blockcore-wallet/main/angular/src/shared/servers.ts");
            urllist.AddRange(from Match item in _regex.Matches(txt)
                             where !urllist.Contains(item.Value)
                             select item.Value);
        }
        urllist.AddRange(from item in await _blockcoreChains.GetAllChains()
                         let blockcoreIndexer = $"https://{item.symbol.ToLower(new CultureInfo("en-US", false))}.indexer.blockcore.net"
                         where !urllist.Contains(blockcoreIndexer)
                         select blockcoreIndexer);

        var Indexerlist = new List<IndexersViewModel>();
        try
        {
            foreach (var indexer in urllist.OrderBy(c => c))
            {
                var indexerWithStatus = new IndexersViewModel();
                indexerWithStatus.Url = indexer;
                indexerWithStatus.IsActive = await PingIndexer(indexer + "/api/stats/heartbeat");

                if (indexerWithStatus.IsActive)
                {
                    var location = await GetIndexerLocation(indexer);
                    if (location != null)
                    {
                        indexerWithStatus.Location = location;
                    }
                }

                Indexerlist.Add(indexerWithStatus);
            }
            return Indexerlist;
        }
        catch (Exception ex)
        {
            return Indexerlist;
        }
    }

    public async Task<bool> PingIndexer(string indexerUrl)
    {
        try
        {
            using (HttpClient _httpClient = new HttpClient())
            {
                var responseMessage = await _httpClient.GetAsync(new Uri(indexerUrl));
                return responseMessage.IsSuccessStatusCode;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<IndexerLocationViewModel> GetIndexerLocation(string indexerUrl)
    {
        try
        {
            var location = await new JsonToObjects<IndexerLocationViewModel>().DownloadAndConverToObjectAsync("http://ip-api.com/json/" + indexerUrl.Replace("https://", "", StringComparison.Ordinal));
            return location;
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<IndexersViewModel>> GetIndexers(int page = 1, int pageSize = 15)
    {
        var urllist = new List<string>();
        using (HttpClient _httpClient = new HttpClient())
        {
            string txt = await _httpClient.GetStringAsync("https://raw.githubusercontent.com/block-core/blockcore-wallet/main/angular/src/shared/servers.ts");
            urllist.AddRange(from Match item in _regex.Matches(txt)
                             where !urllist.Contains(item.Value)
                             select item.Value);
        }
        urllist.AddRange(from item in await _blockcoreChains.GetAllChains()
                         let blockcoreIndexer = $"https://{item.symbol.ToLower(new CultureInfo("en-US", false))}.indexer.blockcore.net"
                         where !urllist.Contains(blockcoreIndexer) &&
                         !string.Equals(item.symbol, "BTC", StringComparison.Ordinal) && !string.Equals(item.symbol, "HOME", StringComparison.Ordinal)

                         select blockcoreIndexer);
        urllist.Add("https://homecoin.indexer.blockcore.net");
        var totalCount = urllist.Count;

        var pagingList = urllist.OrderBy(c => c).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        var Indexerlist = new List<IndexersViewModel>();
        try
        {
            foreach (var indexer in pagingList.OrderBy(c => c))
            {
                var indexerWithStatus = new IndexersViewModel();
                indexerWithStatus.Url = indexer;
                indexerWithStatus.IsActive = await PingIndexer(indexer + "/api/stats/heartbeat");

                if (indexerWithStatus.IsActive)
                {
                    var location = await GetIndexerLocation(indexer);
                    if (location != null)
                    {
                        indexerWithStatus.Location = location;
                    }
                }



                Indexerlist.Add(indexerWithStatus);
            }
            return Indexerlist;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

}