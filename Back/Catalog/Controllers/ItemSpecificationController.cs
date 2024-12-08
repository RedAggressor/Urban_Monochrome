﻿using Catalog.Host.Models.Dto;
using Catalog.Host.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
    public class ItemSpecificationController : ControllerBase
    {
        private readonly IItemSpecificationService _itemSpecificationService;
        public ItemSpecificationController(IItemSpecificationService itemSpecificationService)
        { 
            _itemSpecificationService = itemSpecificationService;
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddSpecificationAsync(DataRequest<ItemSpecification>? request)
        {
            var response = await _itemSpecificationService.AddSpecificationAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [ValidateRequestBody]
        [ProducesResponseType(typeof(DataResponse<ItemSpecification>), (int)HttpStatusCode.OK)]
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
        [ProducesResponseType(typeof(DataResponse<ItemSpecification>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateSpecificationAsync(DataRequest<ItemSpecification>? request)
        {
            var response = await _itemSpecificationService.UpdateSpecificationAsync(request);
            return Ok(response);
        }
    }
}
