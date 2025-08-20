using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services_Abstraction;
using Shared.MedulesDto;

namespace Presentaion
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController(IServiceManager serviceManager) :ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerRequestDto customerRequestDto)
        {
          var result= await serviceManager.CustomerServices.CreateCustomerAsync(customerRequestDto);
            return Ok(result);
        }
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateCustomer(int Id, CustomerRequestDto customerRequestDto)
        {
            var result =await serviceManager.CustomerServices.UpdateCustomerAsync(Id, customerRequestDto);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerById(int Id)
        {
            var result =await serviceManager.CustomerServices.GetCustomerByIdAsync(Id);
            return Ok(result);
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result =await serviceManager.CustomerServices.GetAllCustomersAsync();
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int Id)
        {
            var result =await serviceManager.CustomerServices.DeleteCustomerAsync(Id);
            return Ok(result);
        }
    }
}
