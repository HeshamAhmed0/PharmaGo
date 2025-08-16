using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Moduls
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        
        public Customer Customer { get; set; }
    }
}
