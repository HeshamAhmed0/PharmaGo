using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Moduls.BasketModels;
using Presistance.Reposatories;
using Services_Abstraction;
using Shared.MedulesDto.BasketDtos;

namespace Services
{
    public class BasketService(IProductService productService,
                                IBasketReposatory basketReposatory,
                                IMapper mapper) : IBasketSerrvice
    {
        public async Task<BasketResponseDto> AddItemToBasket(string CustomerId, AddBasketDto basketDto)
        {
            var product = await productService.GetProductByIdAsync(basketDto.ProductId);
            if (product == null) throw new Exception("Product With Id Is Not Found !!");

            var basket = await basketReposatory.GetBasketById(CustomerId);
            if (basket == null)
            {
                basket = new CustomerBasket(CustomerId);
            }
            var CheckForItem = basket.Items.FirstOrDefault(x => x.ProductId == basketDto.ProductId);
            if (CheckForItem == null)
            {
                basket.Items.Add(new BasketItem()
                {
                    ProductId = product.Id,
                    ProductName = product.ProductName,
                    PictureUrl = product.PictureUrl,
                    Price = product.ProductPrice,
                    Quantity = basketDto.Quantity
                });
            }
            else
            {
                CheckForItem.Quantity += basketDto.Quantity;
            }
            return new BasketResponseDto
            {
                Id = CustomerId,
                Items = basket.Items.Select(i => new BasketItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    PictureUrl = i.PictureUrl,
                    Price = i.Price,
                    Quantity = i.Quantity
                }).ToList()
            };
       }
        public async Task<BasketResponseDto> GetBasket(string CustomerId)
        {
           var basket = await basketReposatory.GetBasketById(CustomerId);
          var result = mapper.Map<BasketResponseDto>(basket);
            if (result == null) throw new Exception("This Error From Mapping !!");
            return result;
        }
        public async Task<bool> DeleteItem(string CustomerId)
        {
           var result = await basketReposatory.DeleteBasketById(CustomerId);
            return result ?true : false;
        }
    }
}
