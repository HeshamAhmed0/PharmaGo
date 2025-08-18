using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Moduls;
using Presistance;
using Services_Abstraction;
using Shared.MedulesDto;

namespace Services
{
    public class CustomerServices(IUnitofwork unitofwork,
                                  StoreDbContext storeDbContext,
                                  IMapper mapper) : ICustomerServices
    {
        public async Task<CustomerResultDto> CreateCustomerAsync(CustomerRequestDto customerRequestDto)
        {
            if (customerRequestDto is null)  throw new Exception($"Customer Request Not Found !!");
            var CustomerMapping = mapper.Map<Customer>(customerRequestDto);
            if (CustomerMapping is null) throw new Exception($"Customer Mapping Is Not Correct");
            await unitofwork.GenericReposatory<Customer,int>().AddAsync(CustomerMapping);
            var CustomerResult =mapper.Map<CustomerResultDto>(CustomerMapping);
            return CustomerResult;
        }
        public Task<CustomerResultDto> UpdateCustomerAsync(int Id, CustomerRequestDto customerRequestDto)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<CustomerResultDto>> GetAllCustomersAsync()
        {
            throw new NotImplementedException();
        }
        public Task<CustomerResultDto> GetCustomerByIdAsync(int ProductID)
        {
            throw new NotImplementedException();
        }

    }
}
