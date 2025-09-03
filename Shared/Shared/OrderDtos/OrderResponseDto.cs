using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderDtos
{
    public class OrderResponseDto
    {
       public Guid OredrId { get; set; }
       public string CustomerEmail { get; set; }
       public DateTime OrderDate { get; set; }

       public ShippingAddressDto ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentStatus { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }


    }
}
