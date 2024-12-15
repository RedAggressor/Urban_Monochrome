using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data
{
    public static class InitializeDatabase
    {
        public static async Task Initialize(CatalogDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Types.Any())
            {
                await context.Types.AddRangeAsync(GetPreconfiguredTypes());

                await context.SaveChangesAsync();
            }

            if (!context.Groupes.Any())
            {
                await context.Groupes.AddRangeAsync(GetPreconfiguredGroupes());

                await context.SaveChangesAsync();
            }

            if (!context.Items.Any())
            {
                await context.Items.AddRangeAsync(GetPreconfiguredItems());

                await context.SaveChangesAsync();
            }

            if (!context.Colors.Any())
            {
                await context.Colors.AddRangeAsync(GetPreconfiguredColors());

                await context.SaveChangesAsync();
            }

            if (!context.Sizes.Any())
            {
                await context.Sizes.AddRangeAsync(GetPreconfiguredSizes());

                await context.SaveChangesAsync();
            }

            if (!context.UniqueItems.Any())
            {
                await context.UniqueItems.AddRangeAsync(GetPreconfiguredItemSize());

                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<UniqueItemEntity> GetPreconfiguredItemSize()
        {
            return new List<UniqueItemEntity>
            {
                new UniqueItemEntity
                {
                    SizeId = 1,
                    ItemId = 1,
                    ColorId = 1,
                    Quantity = 100
                },
                new UniqueItemEntity
                {
                    SizeId = 2,
                    ItemId = 1,
                    ColorId = 2,
                    Quantity = 100
                },
                new UniqueItemEntity
                {
                    SizeId = 3,
                    ItemId = 1,
                    ColorId = 2,
                    Quantity = 100
                },
                new UniqueItemEntity
                {
                    SizeId = 4,
                    ItemId = 1,
                    ColorId = 2,
                    Quantity = 100
                },
                new UniqueItemEntity
                {
                    SizeId = 5,
                    ItemId = 1,
                    ColorId = 1,
                    Quantity = 100
                },
                new UniqueItemEntity
                {
                    SizeId = 6,
                    ItemId = 1,
                    ColorId = 1,
                    Quantity = 100
                },
                new UniqueItemEntity
                {
                    SizeId = 1,
                    ItemId = 2,
                    ColorId = 2,
                    Quantity = 100
                },
                new UniqueItemEntity
                {
                    SizeId = 2,
                    ItemId = 2,
                    ColorId = 2,
                    Quantity = 100
                },
                new UniqueItemEntity
                {
                    SizeId = 3,
                    ItemId = 2,
                    ColorId = 1,
                    Quantity = 100
                }
            };
        }

        private static IEnumerable<GroupeEntity> GetPreconfiguredGroupes()
        {
            return new List<GroupeEntity>()
            {
                new GroupeEntity(){ Name = "Dark Pulse", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new GroupeEntity(){ Name = "Duality",  CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new GroupeEntity(){ Name = "Pure Essence",  CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new GroupeEntity(){ Name = "Total Black" ,  CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            };
        }

        private static IEnumerable<TypeEntity> GetPreconfiguredTypes()
        {
            return new List<TypeEntity>()
            {
                new TypeEntity(){ Name = "Dresses" },
                new TypeEntity(){ Name = "Hoodies" },
                new TypeEntity(){ Name = "Jeans" },
                new TypeEntity(){ Name = "Outerwear" },
                new TypeEntity(){ Name = "Pants" },
                new TypeEntity(){ Name = "T-shirts" },
                new TypeEntity(){ Name = "Tops" }
            };
        }

        private static IEnumerable<SizeEntity> GetPreconfiguredSizes()
        {
            return new List<SizeEntity>
            {
                new SizeEntity(){Name = "XS"},
                new SizeEntity(){Name = "S"},
                new SizeEntity(){Name = "M"},
                new SizeEntity(){Name = "L" },
                new SizeEntity(){Name = "XL"},
                new SizeEntity(){Name = "XXL"}
            };
        }

        private static IEnumerable<ColorEntity> GetPreconfiguredColors()
        {
            return new List<ColorEntity>
            {
                new ColorEntity{Name = "Black"},
                new ColorEntity{Name = "White"}
            };
        }

        private static IEnumerable<ItemEntity> GetPreconfiguredItems()
        {
            return new List<ItemEntity>()
            {
                new ItemEntity { Description = "strawberry.discription", Name = "strawberry", Price = 19.5, ImageUrl = "strawberry.png", TypeId  = 1, GroupeId = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow},
                new ItemEntity { Description = "bilberry.discription", Name = "bilberry", Price = 8.50, ImageUrl = "bilberry.png", TypeId  = 2, GroupeId = 3, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow},
                new ItemEntity { Description = "mango.discription", Name = "mango", Price = 6.50, ImageUrl = "mango.png", TypeId  = 3, GroupeId = 4, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new ItemEntity { Description = "kiwi.discription", Name = "kiwi", Price = 10.50, ImageUrl = "kiwi.png", TypeId  = 1, GroupeId = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            };
        }
    }
}