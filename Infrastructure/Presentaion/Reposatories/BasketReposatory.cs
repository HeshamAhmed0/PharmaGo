using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Moduls.BasketModels;
using StackExchange.Redis;

namespace Presistance.Reposatories
{
    public class BasketReposatory(IConnectionMultiplexer connectionMultiplexer) : IBasketReposatory
    {
        private readonly IDatabase database = connectionMultiplexer.GetDatabase();

        public async Task<CustomerBasket> GetBasketById(string Id)
        {
           var redis =await database.StringGetAsync(Id);
            if (string.IsNullOrEmpty(redis)) return null!;

            var basket =JsonSerializer.Deserialize<CustomerBasket>(redis);
            if(basket == null) return null!; 
            return basket;
        }

        public async Task<CustomerBasket> UpdateBasketById(CustomerBasket basket, TimeSpan? timeSpan)
        {
            var basketAsJson =JsonSerializer.Serialize(basket);
            var SetAtRedis = await database.StringSetAsync(basket.Id, basketAsJson, TimeSpan.FromDays(20));
            if (SetAtRedis == false) return null!;
            return await GetBasketById(basket.Id);
        }
        public async Task<bool> DeleteBasketById(string Id)
        {
           return await database.KeyDeleteAsync(Id);
        }
    }
}
