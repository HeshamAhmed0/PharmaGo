using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Moduls.OrderModuls
{
    public class Order : BaseEntity<Guid>
    {
            public Customer Customer { get; set; }
            public string CustomerId {  get; set; }

            [EmailAddress]
            public string BuyerEmail { get; set; }
            public DateTime OrderDate { get; set; } = DateTime.UtcNow;

            public ShippingAdress ShippingAddress { get; set; }

            public DeliveryMethod DeliveryMethod { get; set; }
            public int DeliveryMethodId { get; set; }


        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

            public decimal Subtotal { get; set; }
            public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

            public decimal GetTotal()
            {
                decimal deliveryCost = DeliveryMethod switch
                {
                    DeliveryMethod.Standard => 10,
                    DeliveryMethod.Express => 25,
                    DeliveryMethod.Overnight => 50,
                    _ => 0
                };

                return Subtotal + deliveryCost;
            }
    }

}


