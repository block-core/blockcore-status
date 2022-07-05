using BlockcoreStatus.DataLayer.Context;
using BlockcoreStatus.Entities;
using BlockcoreStatus.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Linq;
using BlockcoreStatus.ViewModels.Indexers;
using BlockcoreStatus.Common.WebToolkit;
using Blockcore.Status.Entities.Indexer;
using Microsoft.Extensions.Options;
using BlockcoreStatus.ViewModels.Admin.Settings;
using Blockcore.Status.ViewModels.Indexers;
using Newtonsoft.Json;

namespace BlockcoreStatus.Services;

public class EfBlockcoreIndexersService : IBlockcoreIndexersService
{

    private readonly IBlockcoreChainsService _blockcoreChains;
    private readonly DbSet<BlockcoreIndexers> blockcoreIndexers;

    private readonly IUnitOfWork _uow;
    private readonly IOptionsSnapshot<SiteSettings> _siteOptions;

    public EfBlockcoreIndexersService(IUnitOfWork uow, IBlockcoreChainsService blockcoreChains, IOptionsSnapshot<SiteSettings> siteOptions)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _blockcoreChains = blockcoreChains;
        blockcoreIndexers = _uow.Set<BlockcoreIndexers>();
        _siteOptions = siteOptions ?? throw new ArgumentNullException(nameof(siteOptions));

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

    public async Task AddOrUpdateIndexerToDB()
    {
        var blockcore_indexer_list = new List<string>();

        blockcore_indexer_list.AddRange(from item in await _blockcoreChains.GetAllChains()
                                        let blockcoreIndexer = $"https://{item.symbol.ToLower(new CultureInfo("en-US", false))}.indexer.blockcore.net"
                                        where !blockcore_indexer_list.Contains(blockcoreIndexer) &&
                                        !string.Equals(item.symbol, "BTC", StringComparison.Ordinal) && !string.Equals(item.symbol, "HOME", StringComparison.Ordinal)
                                        select blockcoreIndexer);
        blockcore_indexer_list.Add("https://homecoin.indexer.blockcore.net");


        foreach (var url in blockcore_indexer_list.OrderBy(c => c))
        {
            var indexer = new BlockcoreIndexers();
            indexer.Url = url;
            using (HttpClient _httpClient = new HttpClient())
            {
                try
                {
                    var responseMessage = await _httpClient.GetAsync(new Uri(url + "/api/stats/heartbeat"));
                    indexer.Online = responseMessage.IsSuccessStatusCode;
                }
                catch
                {
                    indexer.Online = false;
                }
            }

            if (indexer.Online)
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
                    _indexer.Online = indexer.Online;
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

    public async Task<List<IndexersViewModel>> GetAllIndexerFromDB()
    {
        try
        {
            var allIndexers = new List<IndexersViewModel>();

            var blockcoreDomainIndexers = await blockcoreIndexers.ToListAsync();

            allIndexers.Add(new IndexersViewModel() { NameServer = "blockcore.net", Indexers = blockcoreDomainIndexers });

            string dns_service = _siteOptions.Value.BlockcoreDNS.Url;
            var ns_list = new List<DNSServiceViewModel>();
            var all_ns = await new JsonToObjects<IReadOnlyList<DNSServiceViewModel>>().DownloadAndConverToObjectAsync(dns_service);
            if (all_ns.Count > 0)
            {
                foreach (var ns in all_ns)
                {
                    if (await PingIndexer(ns.DnsServer + "/api/dns/ipaddress"))
                    {
                        ns_list.Add(ns);
                    }
                }
            }


            foreach (var ns in ns_list.Select(c => c.DnsServer))
            {
                var ns_indexer_list = new List<BlockcoreIndexers>();
                var services = await new JsonToObjects<List<dynamic>>().DownloadAndConverToObjectAsync(ns + "/api/dns/services/service/Indexer");
                foreach (var indexer in services)
                {
                    var _indexer = new BlockcoreIndexers() { Url = indexer.domain, Online = indexer.online };
                    var location = await GetIndexerLocation("https://" + indexer.domain);
                    if (location != null)
                    {
                        if (string.Equals(location.status, "success", StringComparison.Ordinal))
                        {
                            _indexer.Status = location.status;
                            _indexer.Org = location.org;
                            _indexer.Country = location.country;
                            _indexer.CountryCode = location.countryCode;
                            _indexer.Region = location.region;
                            _indexer.RegionName = location.regionName;
                            _indexer.City = location.city;
                            _indexer.Zip = location.zip;
                            _indexer.Lat = location.lat;
                            _indexer.Lon = location.lon;
                            _indexer.Timezone = location.timezone;
                            _indexer.Isp = location.isp;
                            _indexer.Query = location.query;
                        }
                    }
                    ns_indexer_list.Add(_indexer);
                }
                allIndexers.Add(new IndexersViewModel() { NameServer = ns, Indexers = ns_indexer_list });
            }
            return allIndexers;
        }
        catch
        {
            return null;
        }
    }

}