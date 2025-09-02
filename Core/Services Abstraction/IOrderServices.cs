using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.OrderDtos;

namespace Services_Abstraction
{
    public interface IOrderServices
    {
        public Task<OrderResponseDto> CreateOrder(string Email ,string BasketId,CreateOrderDto createOrderDto);
    }
}
