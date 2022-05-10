using blockcore.status.DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace blockcore.status.DataLayer.InMemoryDatabase;

public class InMemoryDatabaseContext : ApplicationDbContext
{
    public InMemoryDatabaseContext(DbContextOptions options) : base(options)
    {
    }
}