using blockcore.status.Entities.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blockcore.status.DataLayer.Configurations;

public class AppDataProtectionKeyConfiguration : IEntityTypeConfiguration<AppDataProtectionKey>
{
    public void Configure(EntityTypeBuilder<AppDataProtectionKey> builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.ToTable("AppDataProtectionKeys");
        builder.HasIndex(e => e.FriendlyName).IsUnique();
    }
}