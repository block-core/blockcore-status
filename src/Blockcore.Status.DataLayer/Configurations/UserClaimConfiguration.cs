using BlockcoreStatus.Entities.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockcoreStatus.DataLayer.Configurations;

public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.HasOne(userClaim => userClaim.User)
            .WithMany(user => user.Claims)
            .HasForeignKey(userClaim => userClaim.UserId);

        builder.ToTable("AppUserClaims");
    }
}