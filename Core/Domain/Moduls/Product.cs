using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Moduls
{
    public class Product :BaseEntity<int>
    {
        public string ProductName { get; set; } = null!;

        public string ProductDescription { get; set; } = null!;

        public decimal ProductPrice { get; set; } 

        public string PictureUrl { get; set; } = null!;
        public string Company_name { get; set; } = null!;
        public DateTime Expiry_Date { get; set; }
        public string Quantity { get; set; } = null!;


    }
}
