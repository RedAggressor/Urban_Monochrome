using Order.Host.Data.Entities;

namespace Order.Host.Data
{
    public static class InitialDbOrder
    {
        public static async Task Initialize(OrderDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Orders.Any())
            {
                await context.Orders.AddRangeAsync(GetPreconfiguredOrders());

                await context.SaveChangesAsync();
            }
            if (!context.OrderItems.Any())
            {
                await context.OrderItems.AddRangeAsync(GetPReconfiguredOrderItems());

                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<OrderEntity> GetPreconfiguredOrders()
        {
            return new List<OrderEntity>()
            {
                new OrderEntity() { Id = "1", UserId = "1", StatusOrder = "Create"}
            };
        }

        private static IEnumerable<OrderItemEntity> GetPReconfiguredOrderItems()
        {
            return new List<OrderItemEntity>()
            { 
                new OrderItemEntity() { Count = 1, OrderId = "1", ItemId = 1 },
                new OrderItemEntity() { Count = 2, OrderId = "1", ItemId = 2 }
            };
        }
    }
}
