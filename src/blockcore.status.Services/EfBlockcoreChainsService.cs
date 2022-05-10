using blockcore.status.DataLayer.Context;
using blockcore.status.Entities;
using blockcore.status.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace blockcore.status.Services;

public class EfBlockcoreChainsService : IBlockcoreChainsService
{
    private readonly IUnitOfWork _uow;

    public EfBlockcoreChainsService(IUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));

    }

}