using Consumer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consumer.Infrastructure.Data.ConfigurationEntities;
public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasMany(i => i.Additions)
               .WithOne()
               .HasForeignKey(a => a.ItemId);
        builder.HasMany(i => i.Options)
               .WithOne()
               .HasForeignKey(o => o.ItemId);
    }
}