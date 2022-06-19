using BlockcoreStatus.DataLayer.Context;
using BlockcoreStatus.Entities;
using BlockcoreStatus.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Linq;
using BlockcoreStatus.ViewModels.Indexers;
using BlockcoreStatus.Common.WebToolkit;
using Blockcore.Status.Entities.Indexer;

namespace BlockcoreStatus.Services;

public class EfBlockcoreIndexersService : IBlockcoreIndexersService
{

    private readonly IBlockcoreChainsService _blockcoreChains;
    private static readonly Regex _regex = new Regex(@"(http|ftp|https):\/\/([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?");
    private readonly DbSet<BlockcoreIndexers> blockcoreIndexers;
    private readonly IUnitOfWork _uow;
    public EfBlockcoreIndexersService(IUnitOfWork uow, IBlockcoreChainsService blockcoreChains)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _blockcoreChains = blockcoreChains;
        blockcoreIndexers = _uow.Set<BlockcoreIndexers>();
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

    public async Task AddOrUpdateIndexerToDB()
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

        foreach (var url in urllist.OrderBy(c => c))
        {
            var indexer = new BlockcoreIndexers();
            indexer.Url = url;
            using (HttpClient _httpClient = new HttpClient())
            {
                try
                {
                    var responseMessage = await _httpClient.GetAsync(new Uri(url + "/api/stats/heartbeat"));
                    indexer.IsActive = responseMessage.IsSuccessStatusCode;
                }
                catch
                {
                    indexer.IsActive = false;
                }
            }

            if (indexer.IsActive)
            {
                var location = await GetIndexerLocation(url);
                if (location != null)
                {
                    if (string.Equals(location.status, "success", StringComparison.Ordinal))
                    {
                        indexer.Status = location.status;
                        indexer.Org = location.org;
                        indexer.Country = location.country;
                        indexer.CountryCode = location.countryCode;
                        indexer.Region = location.region;
                        indexer.RegionName = location.regionName;
                        indexer.City = location.city;
                        indexer.Zip = location.zip;
                        indexer.Lat = location.lat;
                        indexer.Lon = location.lon;
                        indexer.Timezone = location.timezone;
                        indexer.Isp = location.isp;
                        indexer.Query = location.query;
                    }
                }
                indexer.FailedPings = 0;
            }
            else
            {
                indexer.FailedPings = 1;
            }
            var isExist = blockcoreIndexers.Where(c => c.Url == url);
            if (await isExist.AnyAsync())
            {
                var _indexer = await isExist.FirstOrDefaultAsync();
                if (_indexer != null)
                {
                    _indexer.IsActive = indexer.IsActive;
                    _indexer.Status = indexer.Status;
                    _indexer.Org = indexer.Org;
                    _indexer.Country = indexer.Country;
                    _indexer.CountryCode = indexer.CountryCode;
                    _indexer.Region = indexer.Region;
                    _indexer.RegionName = indexer.RegionName;
                    _indexer.City = indexer.City;
                    _indexer.Zip = indexer.Zip;
                    _indexer.Lat = indexer.Lat;
                    _indexer.Lon = indexer.Lon;
                    _indexer.Timezone = indexer.Timezone;
                    _indexer.Isp = indexer.Isp;
                    _indexer.Query = indexer.Query;
                    if (_indexer.FailedPings <= 50)
                    {
                        _indexer.FailedPings += indexer.FailedPings;
                    }
                    blockcoreIndexers.Update(_indexer);
                }
            }
            else
            {
                blockcoreIndexers.Add(indexer);
            }
            await _uow.SaveChangesAsync();
            await Task.Delay(1000);

        }
    }

    public async Task<List<BlockcoreIndexers>> GetAllIndexerFromDB()
    {
        try
        {
            return await blockcoreIndexers.ToListAsync();
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<BlockcoreIndexers>> GetIndexerFromDB(int page = 1, int pageSize = 50)
    {
        try
        {
            return await blockcoreIndexers.OrderBy(c => c.Url).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        catch
        {
            return null;
        }
    }
}