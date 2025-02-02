using Catalog.Host.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Scope("catalog.catalogbfs")]
    [Route(ComponentDefaults.DefaultRoute)]
    public class CatalogBFSController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        public CatalogBFSController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(DataResponse<IEnumerable<ItemDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItemsById(DataRequest<List<int>> dataRequest)
        {
            var response = await _catalogService.GetItemsByIdAsync(dataRequest);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DataResponse<IEnumerable<UniqueItemResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSpecificationByIdAsync(DataRequest<List<int>> dataRequest)
        {
            var response = await _catalogService.GetSpecificationsByIdAsync(dataRequest);
            return Ok(response);
        }
    }
}
