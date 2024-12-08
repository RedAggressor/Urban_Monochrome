using Catalog.Host.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
    public class SizeController : ControllerBase
    {
        private readonly ISizeService _sizeService;
        public SizeController(ISizeService sizeService) 
        { 
            _sizeService = sizeService;
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddSizeAsync(DataRequest<string>? request)
        {
            var response = await _sizeService.AddSizeAsync(request!);
            return Ok(response);
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<SizeDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSizeByIdAsync(DataRequest<int>? request)
        {
            var response = await _sizeService.GetSizeByIdAsync(request!);
            return Ok(response);
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteSizebyIdAsync(DataRequest<int>? request)
        {
            var response = await _sizeService.DeleteSizeByIdAsync(request!);
            return Ok(response);
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<SizeDto>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateSizeAsync(DataRequest<SizeDto>? request)
        {
            var response = await _sizeService.UpdateSizeAsync(request!);
            return Ok(response);
        }
    }
}
