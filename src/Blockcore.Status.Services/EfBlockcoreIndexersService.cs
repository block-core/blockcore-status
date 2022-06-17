using BlockcoreStatus.DataLayer.Context;
using BlockcoreStatus.Entities;
using BlockcoreStatus.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Linq;
using BlockcoreStatus.ViewModels.Indexers;

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
        var list = new List<string>();
        using (HttpClient _httpClient = new HttpClient())
        {
            string txt = await _httpClient.GetStringAsync("https://raw.githubusercontent.com/block-core/blockcore-wallet/main/angular/src/shared/servers.ts");
            list.AddRange(from Match item in _regex.Matches(txt)
                          where !list.Contains(item.Value)
                          select item.Value);
        }
        list.AddRange(from item in await _blockcoreChains.GetAllChains()
                      let blockcoreIndexer = $"https://{item.symbol.ToLower(new CultureInfo("en-US", false))}.indexer.blockcore.net"
                      where !list.Contains(blockcoreIndexer)
                      select blockcoreIndexer);
        var Indexerlist = new List<IndexersViewModel>();
        try
        {
            foreach (var indexer in list)
            {
                var indexerWithStatus = new IndexersViewModel();
                indexerWithStatus.IsActive = await PingIndexer(indexer);
                indexerWithStatus.Url = indexer;
          
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
}