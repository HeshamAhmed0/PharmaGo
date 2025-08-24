using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.MedulesDto.AuthModels
{
    public class JwtOptions
    {
        public string SecurityKey { get; set; }
        public string Audiences { get; set; }
        public string Issuer { get; set; }
        public double DirationInDay { get; set; }
    }
}
