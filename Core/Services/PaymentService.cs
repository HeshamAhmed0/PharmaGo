using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Moduls.BasketModels;
using Services_Abstraction;
using Shared.MedulesDto.BasketDtos;
using Stripe;

namespace Services
{
    public class PaymentService(IBasketSerrvice basketSerrvice,
                                IBasketReposatory basketReposatory,
                                IProductService productService) : IPaymentService
    {
        private readonly IBasketSerrvice basketSerrvice = basketSerrvice;

        public async Task<BasketResponseDto> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var basket =await basketSerrvice.GetBasket(BasketId);
            if (basket == null) throw new Exception($"Basket With Id {BasketId} Not Found !!");

            foreach(var item in basket.Items)
            {
                var product =await productService.GetProductByIdAsync(item.ProductId);
                if (product == null) throw new Exception($"Product With Id {item.ProductId} Not Found !!");
                item.Price=product.ProductPrice;
            }

            basket.ShippingAddress = 50;
            var amount=  (long)(basket.Items.Sum(I => I.Price * I.Quantity) + basket.ShippingAddress)*100;

            var service = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                  Amount =amount,
                  Currency="USD",
                  PaymentMethodTypes=new List<string>() { "card"}
                };

                var paymentIntent =await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret=paymentIntent.ClientSecret;
            }
            else
            {
                var Updateoptions = new PaymentIntentUpdateOptions()
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string>() { "card" }
                };

                var paymentIntent = await service.UpdateAsync(basket.PaymentIntentId, Updateoptions);
               
            }

            CustomerBasket customerBasket = new CustomerBasket(BasketId)
            {
                DeliveryMethodId = basket.DeliveryMethodId,
                ClientSecret = basket.ClientSecret,
                Id = basket.Id,
                PaymentIntentId = basket.PaymentIntentId,
                ShippingAddress = basket.ShippingAddress,
                Items = basket.Items.Select(i => new BasketItem
                {
                    ProductId = i.ProductId,
                    ProductName = i.ProductName,
                    Price = i.Price,
                    Quantity = i.Quantity,
                    PictureUrl = i.PictureUrl,
                    Brand = i.Brand,
                    Type = i.Type
                }).ToList()
            };
            var result = await basketReposatory.UpdateBasketById(customerBasket,TimeSpan.FromDays(30));
            if (result is null) throw new Exception("Failed To Update Basket");
            return basket;
        }
    }
}
