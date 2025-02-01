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
    public class UniqueItemController : ControllerBase
    {
        private readonly IItemSpecificationService _itemSpecificationService;
        public UniqueItemController(IItemSpecificationService itemSpecificationService)
        { 
            _itemSpecificationService = itemSpecificationService;
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddSpecificationAsync(DataRequest<UniqueItemResponse>? request)
        {
            var response = await _itemSpecificationService.AddSpecificationAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<UniqueItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSpecificationByIdAsync(DataRequest<int>? request)
        {
            var response = await _itemSpecificationService.GetSpecifictionById(request);
            return Ok(response);
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteSpecificationAsync(DataRequest<int>? request)
        {
            var response = await _itemSpecificationService.DeleteSpecificationAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<UniqueItemResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateSpecificationAsync(DataRequest<UniqueItemResponse>? request)
        {
            var response = await _itemSpecificationService.UpdateSpecificationAsync(request);
            return Ok(response);
        }
    }
}
