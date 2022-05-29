using Microsoft.EntityFrameworkCore;

namespace blockcore.status.DataLayer.Configurations;

public static class IdentityMappings
{
    public static void AddCustomIdentityMappings(this ModelBuilder modelBuilder)
    {
        if (modelBuilder == null)
        {
            throw new ArgumentNullException(nameof(modelBuilder));
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityMappings).Assembly);

        // IEntityTypeConfiguration's which have constructors with parameters
        modelBuilder.ApplyConfiguration(new AppSqlCacheConfiguration());
    }
}