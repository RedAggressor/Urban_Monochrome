using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data
{
    public static class InitializeDatabase
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Items.Any())
            {
                await context.Items.AddRangeAsync(GetPreconfiguredItems());

                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<ItemEntity> GetPreconfiguredItems()
        {
            return new List<ItemEntity>()
            {
                new ItemEntity { Description = "strawberry.discription", Name = "strawberry", Price = 19.5, ImageUrl = "strawberry.png", Type  = "short", Color = "red"},
                new ItemEntity { Description = "bilberry.discription", Name = "bilberry", Price = 8.50, ImageUrl = "bilberry.png", Type  = "short", Color= "black" },
                new ItemEntity { Description = "mango.discription", Name = "mango", Price = 6.50, ImageUrl = "mango.png", Type  = "short", Color="yellow" },
                new ItemEntity { Description = "kiwi.discription", Name = "kiwi", Price = 10.50, ImageUrl = "kiwi.png", Type  = "short", Color = "red" }
            };
        }
    }
}