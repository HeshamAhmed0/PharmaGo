using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services_Abstraction;
using Shared.OrderDtos;

namespace Presentaion
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IServiceManager serviceManager) :ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDto createOrderDto)
        {
            var UserEmail = GetUserEmail();
            var BasketId = GetBasketId();
            var result =await serviceManager.OrderServices.CreateOrder(UserEmail, BasketId, createOrderDto);
            if (result == null) throw new Exception("Create Order Failed!!!");
            return Ok(result);
        }
        private string GetUserEmail()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
                throw new Exception("User email not found in token");
            return email;
        }
        private string GetBasketId()
        {
            var Id = User.FindFirstValue("UserId");
            if (string.IsNullOrEmpty(Id))
                throw new Exception("Basket Id not found in token");
            return Id;
        }
    }
}
