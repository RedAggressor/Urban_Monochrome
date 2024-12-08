using Catalog.Host.Data.Entities;
using Catalog.Host.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) 
            : base(options) { }

        public DbSet<ItemEntity> Items { get; set; } = null!;
        public DbSet<TypeEntity> Types { get; set; } = null!;
        public DbSet<GroupeEntity> Groupes { get; set; } = null!;
        public DbSet<SizeEntity> Sizes { get; set; } = null!;
        public DbSet<ColorEntity> Colors { get; set; } = null!;
        public DbSet<ItemSpecificationEntity> ItemSpecifications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GroupeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SizeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ColorEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ItemSpecificationEntityConfiguration());
        }
    }
}
