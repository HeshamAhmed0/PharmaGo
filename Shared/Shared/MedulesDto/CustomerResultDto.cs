using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.MedulesDto
{
    public class CustomerResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
