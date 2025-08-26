using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services_Abstraction;
using Shared.MedulesDto;

namespace Presentaion
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("Create")]
        //[Authorize(Roles = "Admin")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequestDto product)
        {
            var result =await serviceManager.ProductService.CreateProductAsync(product);
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProdctById([FromRoute]int id)
        {
            var result =await serviceManager.ProductService.GetProductByIdAsync(id);
            return Ok(result);
        }
        [HttpGet("All")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await serviceManager.ProductService.GetAllProductsAsync();
            return Ok(result);
        }
        [HttpPut("Update/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct(int  id , ProductRequestDto productRequestDto)
        {
            var result = await serviceManager.ProductService.UpdateProductAsync(id,productRequestDto);
            return Ok(result);
        }
        [HttpDelete("Delete/{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
             var result = await serviceManager.ProductService.DeleteProductAsync(id);
            return Ok(result);
        }
        
    }
}
