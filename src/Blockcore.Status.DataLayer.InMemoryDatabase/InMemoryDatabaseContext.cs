using BlockcoreStatus.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace BlockcoreStatus.DataLayer.InMemoryDatabase;

public class InMemoryDatabaseContext : ApplicationDbContext
{
    public InMemoryDatabaseContext(DbContextOptions options) : base(options)
    {
    }
}