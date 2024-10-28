using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfigurations
{
    public class NestedTypeEntityConfiguration : IEntityTypeConfiguration<NestedTypeEntity>
    {
        public void Configure(EntityTypeBuilder<NestedTypeEntity> builder)
        {
            builder.ToTable("NestedType");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .UseHiLo("nested_type_hilo")
                .IsRequired();

            builder.HasOne(ci => ci.Type)
               .WithMany(m => m.NestedTypes)
               .HasForeignKey(f => f.TypeId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Property(cb => cb.Name)
                .IsRequired();
        }
    }
}
