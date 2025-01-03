using Microsoft.AspNetCore.Mvc;
using Order.Host.Models.Dto;
using Order.Host.Services.Interfaces;
using System.Net;

namespace Order.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
    public class OrderBffController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderBffController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DataResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateOrderAddItems(DataRequest<List<OrderItemDto?>>? data)
        {
            var userId = HttpContext.Connection.Id;
            var response = await _orderService.AddItemToOrderAsync(userId, data?.Data!);

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DataResponse<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderById(DataRequest<string> data)
        {
            var orderId = data.Data;
            var response = await _orderService.GetOrderByIdAsync(orderId!);

            return Ok(response);    
        }

        [HttpPost]
        [ProducesResponseType(typeof(DataResponse<IEnumerable<OrderDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderByUserId()
        {
            var userId = HttpContext.Connection.Id;
            var response = await _orderService.GetOrdersByUserIdAsync(userId);

            return Ok(response);
        }
    }
}
