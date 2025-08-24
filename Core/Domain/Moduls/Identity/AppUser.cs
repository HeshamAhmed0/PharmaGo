using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Moduls.Identity
{
    public class AppUser :IdentityUser
    {
        public string DisplayName { get; set; }
        public IdentityAddress Address { get; set; }
    }
}
