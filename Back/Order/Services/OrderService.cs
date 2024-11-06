using Order.Host.Dto.Responses;
using Order.Host.Extenctions;
using Order.Host.Models.Dto;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<DataResponse<string>> AddOrderAsync(string userId)
        {
            var result = await _orderRepository.AddOrderAsync(userId);

            return new DataResponse<string>()
            {
                Data = result
            };
        }

        public async Task<DataResponse<string>> AddItemToOrderAsync(
            string userId,
            List<OrderItemDto> orderItems)
        {
            var orderId = await _orderRepository.AddOrderAsync(userId!);

            var orderItemsEntity = orderItems.Select(s => s.MapToOrderItemEntity()).ToList();

            var result = await _orderRepository.AddItemToOrderAsync(orderId, orderItemsEntity!);

            return new DataResponse<string>()
            {
                Data = result
            };
        }

        public async Task<DataResponse<OrderDto>> GetOrderByIdAsync(string orderId)
        {
            var result = await _orderRepository.GetOrderByIdAsync(orderId);

            return new DataResponse<OrderDto>()
            {
                Data = result.MapToOrderDto()
            };
        }

        public async Task<DataResponse<IEnumerable<OrderDto>>> GetOrdersByUserIdAsync(string userId)
        {
            var result = await _orderRepository.GetOrdersByUserId(userId);

            return new DataResponse<IEnumerable<OrderDto>>()
            {
                Data = result.Select(s => s.MapToOrderDto())!
            };
        }
    }
}
