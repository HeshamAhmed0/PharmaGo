using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.MedulesDto.BasketDtos;

namespace Services_Abstraction
{
    public interface IBasketSerrvice
    {
        public Task<BasketResponseDto> AddItemToBasket(string CustomerId , AddBasketDto addBasketDto);
        public Task<BasketResponseDto> GetBasket(string CustomerId);
        public Task<bool> DeleteItem(string CustomerId);
    }
}
