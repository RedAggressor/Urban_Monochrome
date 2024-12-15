using Basket.Host.Models.Requests;
using Basket.Host.Models.Responses;
using Basket.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Host.Controllers
{
    [ApiController]
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
        public async Task<IActionResult> AddDataToCache(DataRequest<UniqueItemResponse?>? data)
        {
            var contextId = HttpContext.Connection.Id;
            var key = $"{_keyControler}{contextId}";

            var response = await _basketService.AddDataAsync(key, data!);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DataResponse<UniqueItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDataFromCache()
        {
            var contextId = HttpContext.Connection.Id;
            var key = $"{_keyControler}{contextId}";

            var response = await _basketService.GetDataAsync(key);
            return Ok(response);
        }
    }
}
