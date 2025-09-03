using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Moduls;
using Domain.Moduls.OrderModuls;
using Services_Abstraction;
using Shared.MedulesDto.BasketDtos;
using Shared.OrderDtos;

namespace Services
{
    public class OrderServices(IBasketSerrvice basketSerrvice,IUnitofwork unitofwork) : IOrderServices
    {

        public async Task<OrderResponseDto> CreateOrder(string Email,string BasketId,CreateOrderDto createOrderDto)
        {
           var Basket =await basketSerrvice.GetBasket(BasketId);
            if (Basket == null || Basket.Items.Count == 0) throw new Exception("There Are Not Items In The Basket ");
            var orderItems = Basket.Items.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList();

            var Order = new Order()
            {
                Id =Guid.NewGuid(),
                CustomerId =BasketId,
                BuyerEmail = Email,
                DeliveryMethodId = createOrderDto.DeliveryMethodId,
                ShippingAddress = new ShippingAdress()
                {
                    City=createOrderDto.ShippingAddress.City,
                    Street=createOrderDto.ShippingAddress.Street,
                    Country=createOrderDto.ShippingAddress.Country,
                    FirstName=createOrderDto.ShippingAddress.FirstName,
                    LastName=createOrderDto.ShippingAddress.LastName,
                },
                OrderDate = DateTime.Now,
                PaymentStatus=PaymentStatus.Pending,
                OrderItems = orderItems,
                Subtotal = orderItems.Sum(item => item.Price * item.Quantity),
                
           
            };
            await unitofwork.GenericReposatory<Order,Guid>().AddAsync(Order);
            await basketSerrvice.DeleteItem(BasketId);
            return new OrderResponseDto()
            {
                OredrId=Order.Id,
                DeliveryMethod = Order.DeliveryMethod.ToString(),
                OrderDate = Order.OrderDate,
                ShippingAddress = createOrderDto.ShippingAddress,
                CustomerEmail = Email,
                Subtotal = orderItems.Sum(item => item.Price * item.Quantity),
                PaymentStatus = Order.PaymentStatus.ToString(),
                Total = orderItems.Sum(item => item.Price * item.Quantity) + 50,
                OrderItems = Order.OrderItems.Select(item => new OrderItemDto
                {
                    PictureUrl = item.PictureUrl,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                }).ToList()


            };

        }
    }
}
