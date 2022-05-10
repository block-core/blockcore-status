using blockcore.status.DataLayer.Context;
using blockcore.status.Entities;
using blockcore.status.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace blockcore.status.Services;

public class EfBlockcoreIndexersService : IBlockcoreIndexersService
{
    private readonly IUnitOfWork _uow;

    public EfBlockcoreIndexersService(IUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));

    }

}