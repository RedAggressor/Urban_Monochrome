using Catalog.Host.Models.Dto;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Responses;
using Catalog.Host.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService) 
        {
            _itemService = itemService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DataResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddItemAsync(DataRequest<ItemDto> data)
        {
            var response = await _itemService.AddItemAsync(data);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DataResponse<ItemDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItemByIdAsync(DataRequest<int> data)
        {
            var response = await _itemService.GetItemByIdAsync(data);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DataResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteItemById(DataRequest<int> data)
        {
            var response = await _itemService.DeleteItemByIdAsync(data);
            return Ok(response);
        }
    }
}
