using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_Abstraction
{
    public interface IServiceManager
    {
        IProductService ProductService { get; }
        ICustomerServices CustomerServices { get; }
        IAuthServices AuthServices { get; }
        IBasketSerrvice BasketSerrvice { get; }
        IOrderServices OrderServices { get; }
    }
}
