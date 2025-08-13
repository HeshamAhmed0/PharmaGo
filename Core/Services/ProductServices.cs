using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Moduls;
using Services_Abstraction;
using Shared.MedulesDto;

namespace Services
{
    public class ProductServices(IMapper mapper,
                                 IUnitofwork unitofwork) : IProductService
    {
        public async Task<ProductResultDto> CreateProductAsync(ProductRequestDto productRequest)
        {
            if (productRequest == null)
                throw new ProductCreate();

            var product = mapper.Map<Product>(productRequest);
            if (product == null)
                throw new ProductCreate();

            await unitofwork.GenericReposatory<Product, int>().AddAsync(product);

            var productResultDto = mapper.Map<ProductResultDto>(product);
            return productResultDto;
        }
        public async Task<ProductResultDto> GetProductByIdAsync(int ProductID)
        {
            var product=await  unitofwork.GenericReposatory<Product,int>().GetByIdAsync(ProductID);
            if (product is null) throw new ProductExceprions($"Product With Id : {ProductID} Not Found");
            var producrResultDto =mapper.Map<ProductResultDto>(product);
            return producrResultDto;
        }
        public async Task<bool> DeleteProductAsync(int Id)
        {
            var product = await unitofwork.GenericReposatory<Product,int>().GetByIdAsync(Id);
            if (product is null) throw new ProductExceprions($"Product With Id : {Id} Not Found"); 
            
            
           var result = await unitofwork.GenericReposatory<Product, int>().DeleteAsync(product);
            if(result == false) return false;
            return true;
        }
        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
           var result =await unitofwork.GenericReposatory<Product,int>().GetAllAsync();
            var product =mapper.Map<IEnumerable<ProductResultDto>>(result);
            return product;
        }
        public async Task<ProductResultDto> UpdateProductAsync(int Id,ProductRequestDto productRequestDto)
        {
            var product =await unitofwork.GenericReposatory<Product,int>().GetByIdAsync(Id);
            if (product is null) throw new ProductExceprions($"Product With {Id} Not Found");
             product.ProductDescription=productRequestDto.ProductDescription;
             product.ProductName=productRequestDto.ProductName;
             product.PictureUrl=productRequestDto.PictureUrl;
             product.ProductPrice=productRequestDto.ProductPrice;
             await unitofwork.GenericReposatory<Product, int>().UpadateAsync(product);
            var productResultDto = mapper.Map<ProductResultDto>(product);
            return productResultDto;
        }
    }
}
