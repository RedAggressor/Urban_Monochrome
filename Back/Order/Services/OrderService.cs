using Order.Host.Data.Entities;
using Order.Host.Extenctions;
using Order.Host.Models.Dto;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICatalogHttpService _catalogHttpService;
        public OrderService(
            IOrderRepository orderRepository,
            ICatalogHttpService catalogHttpService)
        {
            _orderRepository = orderRepository;
            _catalogHttpService = catalogHttpService;
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

            var dictinaryItems = await SubstitutionIdToItem(result);

            return new DataResponse<OrderDto>()
            {
                Data = result.MapToOrderDto(dictinaryItems)
            };
        }

        public async Task<DataResponse<IEnumerable<OrderDto>>> GetOrdersByUserIdAsync(string userId)
        {
            var result = await _orderRepository.GetOrdersByUserId(userId);

            var dictinaryItems = await SubstitutionIdToItem(result);

            return new DataResponse<IEnumerable<OrderDto>>()
            {
                Data = result.Select(s => s.MapToOrderDto(dictinaryItems))!
            };
        }
        
        private Dictionary<int, ItemDto> ConvertItemsIdToItem(ICollection<ItemDto> Items)
        {
            return Items.ToDictionary(item => item.Id);
        }

        private List<int> GetItemsId(ICollection<OrderEntity> orders)
        {
            return orders
                .SelectMany(order => order.OrderItems)
                .Select(item => item.ItemId)
                .Distinct()
                .ToList();
        }

        private List<int> GetItemsId(OrderEntity order)
        {
            return order.OrderItems
                .Select(item => item.ItemId)
                .Distinct()
                .ToList();
        }

        private async Task<Dictionary<int, ItemDto>> SubstitutionIdToItem(ICollection<OrderEntity> orders)
        {
            var listItemId = GetItemsId(orders);

            var listItems = await _catalogHttpService.GetItemsByIdAsync(listItemId);

            return ConvertItemsIdToItem(listItems);
        }

        private async Task<Dictionary<int, ItemDto>> SubstitutionIdToItem(OrderEntity orders)
        {
            var listItemId = GetItemsId(orders);
                        
            var listItems = await _catalogHttpService.GetItemsByIdAsync(listItemId);

            return ConvertItemsIdToItem(listItems);
        }
    }
}
