using blockcore.status.DataLayer.Context;
using blockcore.status.Entities;
using blockcore.status.Services.Contracts;
using blockcore.status.ViewModels.Admin.Settings;
using blockcore.status.ViewModels.Chains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using blockcore.status.Common.WebToolkit;

namespace blockcore.status.Services;

public class EfBlockcoreChainsService : IBlockcoreChainsService
{
    private readonly IUnitOfWork _uow;
    private readonly IOptionsSnapshot<SiteSettings> _siteOptions;

    public EfBlockcoreChainsService(IUnitOfWork uow, IOptionsSnapshot<SiteSettings> siteOptions)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        _siteOptions = siteOptions ?? throw new ArgumentNullException(nameof(siteOptions));

    }

    public async Task<IReadOnlyList<ChainsViewModel>> GetAllChains()
    {
        string CHAINS_URL = _siteOptions.Value.BlockcoreChains.ChainsUrl;
        try
        {
            return await new JsonToObjects<IReadOnlyList<ChainsViewModel>>().DownloadAndConverToObjectAsync(CHAINS_URL);
        }
        catch
        {
            return null;
        }

    }


}