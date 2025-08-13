using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Services_Abstraction;

namespace Services
{
    public class ServiceManager(IMapper mapper,IUnitofwork unitofwork) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductServices(mapper,unitofwork);
    }
}
