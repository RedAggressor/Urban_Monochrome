using Catalog.Host.Data.Entities;
using Catalog.Host.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public DbSet<ItemEntity> Items { get; set; } = null!;
        public DbSet<TypeEntity> Types { get; set; } = null!;
        public DbSet<NestedTypeEntity> NestedTypes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new NestedTypeEntityConfiguration());
        }
    }
}
