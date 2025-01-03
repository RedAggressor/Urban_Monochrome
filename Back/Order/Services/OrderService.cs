﻿using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Extenctions;
using Order.Host.Models.Dto;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services
{
    public class OrderService : BaseDataService<OrderDbContext>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICatalogHttpService _catalogHttpService;
        public OrderService(
            ILogger<OrderService> logger,
            IDbContextWrapper<OrderDbContext> dbContextWrapper,
            IOrderRepository orderRepository,
            ICatalogHttpService catalogHttpService)
            : base(dbContextWrapper, logger)
        {
            _orderRepository = orderRepository;
            _catalogHttpService = catalogHttpService;
        }

        public async Task<DataResponse<string>> AddOrderAsync(string userId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _orderRepository.AddOrderAsync(userId);

                return new DataResponse<string>()
                {
                    Data = result
                };
            });            
        }

        public async Task<DataResponse<string>> AddItemToOrderAsync(
            string userId,
            List<OrderItemDto> orderItems)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var orderId = await _orderRepository.AddOrderAsync(userId!);

                var orderItemsEntity = orderItems.Select(s => s.MapToOrderItemEntity()).ToList();

                var result = await _orderRepository.AddItemToOrderAsync(orderId, orderItemsEntity!);

                return new DataResponse<string>()
                {
                    Data = result
                };
            });
        }

        public async Task<DataResponse<OrderDto>> GetOrderByIdAsync(string orderId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _orderRepository.GetOrderByIdAsync(orderId);

                var dictinaryItems = await SubstitutionIdToItem(result);

                return new DataResponse<OrderDto>()
                {
                    Data = result.MapToOrderDto(dictinaryItems)
                };
            });
        }

        public async Task<DataResponse<IEnumerable<OrderDto>>> GetOrdersByUserIdAsync(string userId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _orderRepository.GetOrdersByUserId(userId);

                var dictinaryItems = await SubstitutionIdToItem(result);

                return new DataResponse<IEnumerable<OrderDto>>()
                {
                    Data = result.Select(s => s.MapToOrderDto(dictinaryItems))!
                };
            });
        }
        
        private Dictionary<int, UniqueItemRequest> ConvertItemsIdToItem(ICollection<UniqueItemRequest> Items)
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

        private async Task<Dictionary<int, UniqueItemRequest>> SubstitutionIdToItem(ICollection<OrderEntity> orders)
        {
            var listItemId = GetItemsId(orders);

            var listItems = await _catalogHttpService.GetSpecificationByIdAsync(listItemId);

            return ConvertItemsIdToItem(listItems);
        }

        private async Task<Dictionary<int, UniqueItemRequest>> SubstitutionIdToItem(OrderEntity orders)
        {
            var listItemId = GetItemsId(orders);
                        
            var listItems = await _catalogHttpService.GetSpecificationByIdAsync(listItemId);

            return ConvertItemsIdToItem(listItems);
        }
    }
}
