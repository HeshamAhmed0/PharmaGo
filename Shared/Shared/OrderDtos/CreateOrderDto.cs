using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderDtos
{
    public class CreateOrderDto
    {
        public int DeliveryMethodId { get; set; }   
        public ShippingAddressDto ShippingAddress { get; set; }
    }
}
