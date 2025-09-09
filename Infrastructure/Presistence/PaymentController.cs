using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services_Abstraction;

namespace Presentaion
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController(IServiceManager serviceManager) :ControllerBase
    {
        [HttpPost("{BasketId}")]
        public async Task<IActionResult> CreateOrUpdatePaymentIntent(string BasketId)
        {
          var result =await  serviceManager.PaymentsService.CreateOrUpdatePaymentIntent(BasketId);
            return Ok(result);
        }
    }
}
