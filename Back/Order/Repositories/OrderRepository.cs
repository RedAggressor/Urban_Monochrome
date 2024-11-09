using Microsoft.EntityFrameworkCore;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Repositories.Interfaces;

namespace Order.Host.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _dbContext;
        public OrderRepository(IDbContextWrapper<OrderDbContext> dbContext)
        {
            _dbContext = dbContext.DbContext;
        }

        public async Task<OrderEntity> GetOrderByIdAsync(string id)
        {
            var result = await _dbContext.Orders
                .Include(i => i.OrderItems)
                .FirstOrDefaultAsync(item => item.Id.Equals(id));

            return result!;
        }

        public async Task<string> AddOrderAsync(string UserId)
        {
            var orderId = Guid.NewGuid().ToString();

            await _dbContext.Orders.AddAsync(new OrderEntity()
            {
                Id = orderId,
                UserId = UserId,
                StatusOrder = "Create",
                CreatedAt = DateTime.Now,
                PaymentStatus = "Processing",
                UpdatedAt = DateTime.Now
            });

            await _dbContext.SaveChangesAsync();

            return orderId;
        }

        public async Task<string> AddItemToOrderAsync(string orderId, List<OrderItemEntity> orderItems)
        {
            await _dbContext.OrderItems.AddRangeAsync(orderItems.Select(s => new OrderItemEntity()
            {
                OrderId = orderId,
                Count = s.Count,
                ItemId = s.ItemId,
                Price = s.Price                 
            }));

            var orderEntity = await _dbContext.Orders
                .Include(i =>i.OrderItems)
                .FirstOrDefaultAsync(o => o.Id.Equals(orderId));

            orderEntity!.StatusOrder = "Complite";
            orderEntity.UpdatedAt = DateTime.Now;
            orderEntity.PaymentStatus = "Processing";

            await _dbContext.SaveChangesAsync();

            return orderId;
        }

        public async Task<ICollection<OrderEntity>> GetOrdersByUserId(string UserId)
        {
            var result = await _dbContext.Orders
                .Include(i => i.OrderItems)
                .Where(item => item.UserId.Equals(UserId))
                .ToListAsync();

            return result;
        }

        public async Task<string> DeleteOrderAsync(string orderId)
        {
            var entity = await _dbContext.Orders
                .FirstOrDefaultAsync(f => f.Id.Equals(orderId));

            var status = _dbContext.Orders.Remove(entity!);

            return status.ToString();
        }
    }
}
