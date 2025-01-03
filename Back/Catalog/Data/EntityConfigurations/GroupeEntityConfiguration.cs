using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfigurations
{
    public class GroupeEntityConfiguration : IEntityTypeConfiguration<GroupeEntity>
    {
        public void Configure(EntityTypeBuilder<GroupeEntity> builder)
        {
            builder.ToTable("Groupe");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .UseHiLo("groupe_hilo")
                .IsRequired();            

            builder.Property(cb => cb.Name)
                .IsRequired();
        }
    }
}
