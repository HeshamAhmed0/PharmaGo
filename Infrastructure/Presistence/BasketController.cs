using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services_Abstraction;
using Shared.MedulesDto.BasketDtos;

namespace Presentaion
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManager serviceManager) :ControllerBase
    {
        [HttpPost("Create")]
        public async Task<IActionResult> CreateOrder([FromBody]AddBasketDto addBasketDto)
        {
            var CustomerId = GetUserId();
            var result =await serviceManager.BasketSerrvice.AddItemToBasket(CustomerId, addBasketDto);    
            return Ok(result);
        }
        [HttpGet("Get")]
        public async Task<IActionResult> GetOrderById()
        {
            var CustomerId = GetUserId();
            var result =await serviceManager.BasketSerrvice.GetBasket(CustomerId);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteOrder()
        {
            var CustomerId = GetUserId();
            var result =await serviceManager.BasketSerrvice.DeleteItem(CustomerId);
            return Ok(result);
        }

        private string GetUserId()
        {
            var userId = User.FindFirst("UserId")?.Value
                      ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                throw new Exception("User Id not found in token");

            return userId;
        }



        [HttpGet("TestClaims")]
        [Authorize]
        public IActionResult TestClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(claims);
        }


    }
}
