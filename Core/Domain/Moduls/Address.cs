using Microsoft.EntityFrameworkCore;

namespace Domain.Moduls
{

    [Owned]
    public class Address 
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}