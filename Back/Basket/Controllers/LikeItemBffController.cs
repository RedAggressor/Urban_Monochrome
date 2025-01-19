﻿using Basket.Host.Models.Requests;
using Basket.Host.Services.Interfaces;
using Infrastucture.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class LikeItemBffController : ControllerBase
    {
        private readonly ILikeItemService _likeItemService;
        private readonly string _keyLike = "LikeItem";
        public LikeItemBffController(ILikeItemService likeItemService) 
        { 
            _likeItemService= likeItemService;
        }

        [HttpPost]        
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddLikeItemsAsync(DataRequest<ItemDto>? data)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Properties.Values.Contains("sub"))?.Value;
            var key = $"{_keyLike}{userId}";
            var response = await _likeItemService.AddLikeItemsAsync(key, data!);

            return Ok(response);
        }

        [HttpPost]        
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetLikeItemsAsync()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Properties.Values.Contains("sub"))?.Value;
            var key = $"{_keyLike}{userId}";
            var response = await _likeItemService.GetLikeItemsAsync(key);

            return Ok(response);
        }
    }
}
