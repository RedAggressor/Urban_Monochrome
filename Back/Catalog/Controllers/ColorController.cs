using Catalog.Host.Models.Dto;
using Catalog.Host.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;
        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddColorAsync(DataRequest<string>? request)
        {            
            var response = await _colorService.AddColorAsync(request!);
            return Ok(response);
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<ColorDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetColorById(DataRequest<int>? request)
        {
            var response = await _colorService.GetColorByIdAsync(request!);
            return Ok(response);
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteColorById(DataRequest<int>? request)
        {
            var response = await _colorService.DeleteColorAsync(request!);
            return Ok(response);
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateColorByIdAsync(DataRequest<UpdateDto>? request)
        {
            var response = await _colorService.UpdateColorAsync(request!);
            return Ok(response);
        }
    }
}
