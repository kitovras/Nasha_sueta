using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OurFuss.Core.Db.Entities.User;

namespace OurFuss.Core.Db.EntityConfigurations;

internal class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Ignore(i => i.EmailConfirmed);

        builder.Ignore(i => i.PhoneNumber);
        builder.Ignore(i => i.PhoneNumberConfirmed);

        builder.Ignore(i => i.LockoutEnd);
        builder.Ignore(i => i.TwoFactorEnabled);
        builder.Ignore(i => i.LockoutEnabled);
        builder.Ignore(i => i.AccessFailedCount);
    }
}
