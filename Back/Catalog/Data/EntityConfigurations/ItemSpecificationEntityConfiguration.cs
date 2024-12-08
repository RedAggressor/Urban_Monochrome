using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfigurations
{
    public class ItemSpecificationEntityConfiguration : IEntityTypeConfiguration<ItemSpecificationEntity>
    {
        public void Configure(EntityTypeBuilder<ItemSpecificationEntity> builder)
        {
            builder.ToTable("ItemSpecification");

            builder.HasKey(s => s.Id);

            builder
                .Property(k => k.Id)
                .UseHiLo("itemSpecification_hilo");

            builder.HasOne(o => o.Item)
                .WithMany(m => m.ItemSpecifications)
                .HasForeignKey(f => f.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Size)
                .WithMany(m => m.ItemSpecifications)
                .HasForeignKey(f => f.SizeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Color)
                .WithMany(m => m.ItemSpecifications)
                .HasForeignKey(f => f.ColorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
