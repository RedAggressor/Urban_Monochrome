using Catalog.Host.Services.Abstractions;
using Infrastucture.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowRoleUserPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService) 
        {
            _itemService = itemService;
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddItemAsync(DataRequest<ItemDto?>? data)
        {
            var response = await _itemService.AddItemAsync(data!);
            return Ok(response);
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<ItemDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItemByIdAsync(DataRequest<int>? data)
        {
            var response = await _itemService.GetItemByIdAsync(data!);
            return Ok(response);
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteItemById(DataRequest<int>? data)
        {
            var response = await _itemService.DeleteItemByIdAsync(data!);
            return Ok(response);
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<ItemDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateOrChageItem(DataRequest<ItemDto>? data)
        {
            var resonse = await _itemService.UpdateOrChangeItemAsync(data!);
            return Ok(resonse);
        }
    }
}
