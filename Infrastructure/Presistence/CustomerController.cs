using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services_Abstraction;
using Shared.MedulesDto;

namespace Presentaion
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController(IServiceManager serviceManager) :ControllerBase
    {
        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(CustomerRequestDto customerRequestDto)
        {
            var UserId =User.FindFirstValue("UserID");
          var result= await serviceManager.CustomerServices.CreateCustomerAsync(UserId,customerRequestDto);
            return Ok(result);
        }
        [HttpPost("UpdateCurrentCustomer")]
        public async Task<IActionResult> UpdateCustomer( CustomerRequestDto customerRequestDto)
        {
            var UserId = User.FindFirstValue("UserID");

            var result =await serviceManager.CustomerServices.UpdateCustomerAsync(UserId, customerRequestDto);
            return Ok(result);
        }
        [HttpGet("GetCurrentCustomer")]
        public async Task<IActionResult> GetCurrentCustomer()
        {
            var UserId = User.FindFirstValue("UserID");
            var result =await serviceManager.CustomerServices.GetCustomerByIdAsync(UserId);
            return Ok(result);
        }
        [HttpGet("AllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result =await serviceManager.CustomerServices.GetAllCustomersAsync();
            return Ok(result);
        }
        [HttpDelete("DeleteCurrentCustomer")]
        public async Task<IActionResult> DeleteCurrentCustomer()
        {
            var UserId = User.FindFirstValue("UserID");

            var result =await serviceManager.CustomerServices.DeleteCustomerAsync(UserId);
            return Ok(result);
        }
    }
}
