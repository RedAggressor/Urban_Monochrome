using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfigurations
{
    public class SizeEntityConfiguration : IEntityTypeConfiguration<SizeEntity>
    {
        public void Configure(EntityTypeBuilder<SizeEntity> builder)
        {
            builder.ToTable("Size");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .UseHiLo("size_hilo")
                .IsRequired();

            builder.Property(cb => cb.Name)
                .IsRequired();
        }
    }
}
