using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Moduls.BasketModels
{
    public class CustomerBasket
    {
       

        // This Id Will Come From Jwt Token 
        public string Id { get; set; } 
        public List<BasketItem> Items { get; set; } = new();
        public CustomerBasket(string id)
        {
            Id = id;
        }
    }
}
