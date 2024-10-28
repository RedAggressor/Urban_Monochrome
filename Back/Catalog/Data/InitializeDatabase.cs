using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data
{
    public static class InitializeDatabase
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if(!context.Types.Any())
            {
                await context.Types.AddRangeAsync(GetPreconfiguredTypes());

                await context.SaveChangesAsync();
            }

            if(!context.NestedTypes.Any())
            {
                await context.NestedTypes.AddRangeAsync(GetPreconfiguredNestedType());

                await context.SaveChangesAsync();
            }

            if (!context.Items.Any())
            {
                await context.Items.AddRangeAsync(GetPreconfiguredItems());

                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<NestedTypeEntity> GetPreconfiguredNestedType()
        {
            return new List<NestedTypeEntity>()
            { 
                new NestedTypeEntity(){ Name = "T-Shirt", TypeId = 1 },
                new NestedTypeEntity(){ Name = "Shirts", TypeId = 1 },
                new NestedTypeEntity(){ Name = "BagPack", TypeId = 2 },
                new NestedTypeEntity(){ Name = "Sneakers", TypeId = 3 }
            };

        }

        private static IEnumerable<TypeEntity> GetPreconfiguredTypes()
        {
            return new List<TypeEntity>()
            {
                new TypeEntity(){ Name = "Clothing" },
                new TypeEntity(){ Name = "Bags" },
                new TypeEntity(){ Name = "Shoes" }

            };
        }

        private static IEnumerable<ItemEntity> GetPreconfiguredItems()
        {
            return new List<ItemEntity>()
            {
                new ItemEntity { Description = "strawberry.discription", Name = "strawberry", Price = 19.5, ImageUrl = "strawberry.png", TypeId  = 1, NestedTypeId = 1, Color = "red"},
                new ItemEntity { Description = "bilberry.discription", Name = "bilberry", Price = 8.50, ImageUrl = "bilberry.png", TypeId  = 2, NestedTypeId = 3, Color= "black" },
                new ItemEntity { Description = "mango.discription", Name = "mango", Price = 6.50, ImageUrl = "mango.png", TypeId  = 3, NestedTypeId = 4, Color="yellow" },
                new ItemEntity { Description = "kiwi.discription", Name = "kiwi", Price = 10.50, ImageUrl = "kiwi.png", TypeId  = 1, NestedTypeId = 2, Color = "red" }
            };
        }
    }
}