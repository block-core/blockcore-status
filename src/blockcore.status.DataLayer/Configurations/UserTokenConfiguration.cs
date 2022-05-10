using blockcore.status.Entities.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace blockcore.status.DataLayer.Configurations;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.HasOne(userToken => userToken.User)
            .WithMany(user => user.UserTokens)
            .HasForeignKey(userToken => userToken.UserId);

        builder.ToTable("AppUserTokens");
    }
}