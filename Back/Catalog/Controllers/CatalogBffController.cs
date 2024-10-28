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
    public class CatalogBffController : ControllerBase
    {        
        private readonly ILogger<CatalogBffController> _logger;
        private readonly ICatalogService _catalogService;

        public CatalogBffController(
            ILogger<CatalogBffController> logger,
            ICatalogService catalogService)
        {
            _logger = logger;
            _catalogService = catalogService;
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(ItemsByPageResponse<ItemDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPagenatedItem(PageInfoRequest? pageInfoRequest)
        {
            var response = await _catalogService.GetOrderItemByPricePage(pageInfoRequest!);
            return Ok(response);
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<ItemDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItemByName(DataRequest<string>? value)
        {
            var response = await _catalogService.GetItdeByNameAsync(value!);
            return Ok(response);
        }

    }
}
