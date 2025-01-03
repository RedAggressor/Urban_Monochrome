using Basket.Host.Models.Dto;
using Basket.Host.Models.Requests;
using Basket.Host.Models.Responses;
using Basket.Host.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;

namespace Basket.Host.Controllers
{
    
    [ApiController]
    [Authorize(Policy = "AllowEndUser")]    
    [Route(ComponentDefaults.DefaultRoute)]    
    public class BasketBffController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly string _keyControler = "BasketControler";
        public BasketBffController(IBasketService basketService)
        {
            _basketService = basketService;
        }        

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddDataToCache(DataRequest<ItemDto?>? data)
        {
            var contextId = HttpContext.Connection.Id;
            var key = $"{_keyControler}{contextId}";

            var response = await _basketService.AddDataAsync(key, data!);
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
