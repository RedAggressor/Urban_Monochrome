using Basket.Host.Models.Dto;
using Basket.Host.Models.Requests;
using Basket.Host.Models.Responses;
using Basket.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Host.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]    
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly string _keyControler = "BasketControler";
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }        

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddDataToCache(DataRequest<ItemDto> data)
        {
            var contextId = HttpContext.Connection.Id;
            var key = $"{_keyControler}{contextId}";

            var response = await _basketService.AddDataAsync(key, data);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DataResponse<ItemDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDataFromCache()
        {
            var contextId = HttpContext.Connection.Id;
            var key = $"{_keyControler}{contextId}";

            var response = await _basketService.GetDataAsync(key);
            return Ok(response);
        }
    }
}