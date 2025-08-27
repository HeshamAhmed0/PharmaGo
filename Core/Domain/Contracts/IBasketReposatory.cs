using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Moduls.BasketModels;

namespace Domain.Contracts
{
    public interface IBasketReposatory
    {
        public Task<CustomerBasket> GetBasketById(string Id);
        public Task<CustomerBasket> UpdateBasketById(CustomerBasket Basket,TimeSpan? timeSpan);
        public Task<bool> DeleteBasketById(string Id);
    }
}
