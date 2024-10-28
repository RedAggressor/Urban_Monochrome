using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfigurations
{
    public class ItemEntityConfiguration : IEntityTypeConfiguration<ItemEntity>
    {
        public void Configure(EntityTypeBuilder<ItemEntity> builder)
        {
            builder.ToTable("Item");

            builder
                .HasKey(k => k.Id);

            builder
                .Property(k => k.Id)
                .UseHiLo("item_hilo");

            builder.HasOne(o=>o.Type)
                .WithMany(m=>m.Items)
                .HasForeignKey(o => o.TypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.NestedType)
               .WithMany(m => m.Items)
               .HasForeignKey(o => o.NestedTypeId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
