using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfigurations
{
    public class UniqueItemEntityConfiguration : IEntityTypeConfiguration<UniqueItemEntity>
    {
        public void Configure(EntityTypeBuilder<UniqueItemEntity> builder)
        {
            builder.ToTable("UniqueItem");

            builder.HasKey(s => s.Id);

            builder
                .Property(k => k.Id)
                .UseHiLo("uniqueItem_hilo");

            builder.HasOne(o => o.Item)
                .WithMany(m => m.UniqueItems)
                .HasForeignKey(f => f.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Size)
                .WithMany(m => m.UniqueItems)
                .HasForeignKey(f => f.SizeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Color)
                .WithMany(m => m.UniqueItems)
                .HasForeignKey(f => f.ColorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
