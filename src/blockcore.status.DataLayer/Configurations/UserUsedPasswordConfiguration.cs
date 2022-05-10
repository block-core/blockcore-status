using blockcore.status.Entities.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blockcore.status.DataLayer.Configurations;

public class UserUsedPasswordConfiguration : IEntityTypeConfiguration<UserUsedPassword>
{
    public void Configure(EntityTypeBuilder<UserUsedPassword> builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.ToTable("AppUserUsedPasswords");

        builder.Property(applicationUserUsedPassword => applicationUserUsedPassword.HashedPassword)
            .HasMaxLength(450)
            .IsRequired();

        builder.HasOne(applicationUserUsedPassword => applicationUserUsedPassword.User)
            .WithMany(applicationUser => applicationUser.UserUsedPasswords);
    }
}