using Order.Host.Dto.Requests;
using Order.Host.Dto.Responses;
using Order.Host.Models.Dto;

namespace Order.Host.Services.Interfaces
{
    public interface IOrderService
    {
        Task<DataResponse<string>> AddOrderAsync(string userId);
        Task<DataResponse<string>> AddItemToOrderAsync(string userId, List<OrderItemDto> orderItems);
        Task<DataResponse<OrderDto>> GetOrderByIdAsync(string orderId);
        Task<DataResponse<IEnumerable<OrderDto>>> GetOrdersByUserIdAsync(string userId);
    }
}
