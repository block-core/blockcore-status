using BlockcoreStatus.DataLayer.Context;
using BlockcoreStatus.Entities;
using BlockcoreStatus.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BlockcoreStatus.Services;

public class EfBlockcoreIndexersService : IBlockcoreIndexersService
{
    private readonly IUnitOfWork _uow;

    public EfBlockcoreIndexersService(IUnitOfWork uow)
    {
        _uow = uow ?? throw new ArgumentNullException(nameof(uow));

    }

}