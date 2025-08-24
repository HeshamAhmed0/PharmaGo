using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services_Abstraction;
using Shared.MedulesDto.AuthModels;

namespace Presentaion
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManager serviceManager) :ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
           var result =await serviceManager.AuthServices.LoginAsync(loginDto);
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result =await serviceManager.AuthServices.RegisterAsync(registerDto);
            return Ok(result);
        }
    }
}
