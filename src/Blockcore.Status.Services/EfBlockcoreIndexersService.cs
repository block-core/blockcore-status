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


    private readonly IOptionsSnapshot<SiteSettings> _siteOptions;

    public EfBlockcoreIndexersService(IOptionsSnapshot<SiteSettings> siteOptions)
    {
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


    public async Task<List<IndexersViewModel>> GetAllIndexer()
    {
        try
        {
            var allIndexers = new List<IndexersViewModel>();

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