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
    }
}
