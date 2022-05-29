using BlockcoreStatus.Entities.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockcoreStatus.DataLayer.Configurations;

public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
{
    public void Configure(EntityTypeBuilder<RoleClaim> builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.HasOne(roleClaim => roleClaim.Role)
            .WithMany(role => role.Claims)
            .HasForeignKey(roleClaim => roleClaim.RoleId);

        builder.ToTable("AppRoleClaims");
    }
}