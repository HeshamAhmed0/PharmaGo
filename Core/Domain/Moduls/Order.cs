using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Moduls
{
    public class Order :BaseEntity<int>
    {
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DeliveryMethod DeliveryMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
