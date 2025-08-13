using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.MedulesDto;

namespace Services_Abstraction
{
    public interface IProductService
    {
        public Task<ProductResultDto> CreateProductAsync(ProductRequestDto productRequest);
        public Task<ProductResultDto> UpdateProductAsync(int Id ,ProductRequestDto productRequestDto);
        public Task<bool> DeleteProductAsync(int id );
        public Task<ProductResultDto> GetProductByIdAsync(int ProductID);
        public Task<IEnumerable<ProductResultDto>> GetAllProductsAsync();
    }
}
