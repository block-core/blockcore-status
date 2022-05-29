using BlockcoreStatus.DataLayer.Context;
using BlockcoreStatus.Entities;
using BlockcoreStatus.Services.Contracts;
using BlockcoreStatus.ViewModels.Admin.Settings;
using BlockcoreStatus.ViewModels.Chains;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using BlockcoreStatus.Common.WebToolkit;

namespace BlockcoreStatus.Services;

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