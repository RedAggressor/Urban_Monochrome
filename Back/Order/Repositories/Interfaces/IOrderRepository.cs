using Order.Host.Data.Entities;

namespace Order.Host.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderEntity> GetOrderByIdAsync(string id);
        Task<string> AddOrderAsync(string UserId);
        Task<ICollection<OrderEntity>> GetOrdersByUserId(string UserId);
        Task<string> AddItemToOrderAsync(string orderId, List<OrderItemEntity> orderItems);
        Task<string> DeleteOrderAsync(string orderId);
    }
}
