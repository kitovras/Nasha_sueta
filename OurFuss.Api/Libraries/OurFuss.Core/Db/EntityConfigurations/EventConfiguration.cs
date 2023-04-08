using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OurFuss.Core.Db;

internal class EventConfiguration : IEntityTypeConfiguration<EventEntity>
{
    public void Configure(EntityTypeBuilder<EventEntity> builder)
    {
        builder.HasKey(hk => hk.Id);
        builder.HasData
        (
           new EventEntity
           {
               Id = 1,
               Name = "Первое событие",
           },
           new EventEntity
           {
               Id = 2,
               Name = "Второе событие",
           }
        );
    }
}