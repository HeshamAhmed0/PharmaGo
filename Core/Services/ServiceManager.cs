using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Moduls.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Presistance;
using Services_Abstraction;
using Shared.MedulesDto.AuthModels;

namespace Services
{
    public class ServiceManager(IMapper mapper,IUnitofwork unitofwork,
                                StoreDbContext storeDbContext,
                                IOptions<JwtOptions> options,
                                UserManager<AppUser> userManager) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductServices(mapper,unitofwork);
        public ICustomerServices CustomerServices { get; } = new CustomerServices(unitofwork,storeDbContext,mapper);

        public IAuthServices AuthServices { get; } = new AuthServices(options, userManager);
    }
}
