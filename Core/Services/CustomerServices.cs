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
        public async Task<CustomerResultDto> UpdateCustomerAsync(int Id, CustomerRequestDto customerRequestDto)
        {
            var customer =await unitofwork.GenericReposatory<Customer,int>().GetByIdAsync(Id);
            if (customer is null) throw new Exception($"User With Id : {Id} Is Not Found");
            var Customermapping =mapper.Map(customerRequestDto, customer);
            await unitofwork.GenericReposatory<Customer, int>().UpadateAsync(Customermapping);
            var result = await unitofwork.GenericReposatory<Customer, int>().GetByIdAsync(Id);
            var mappingForResultDto =mapper.Map<CustomerResultDto>(result);
            return mappingForResultDto;
        }
        public async Task<bool> DeleteCustomerAsync(int id)
        {
           var FindCustomer =await unitofwork.GenericReposatory<Customer,int>().GetByIdAsync(id);
            if (FindCustomer is null) throw new Exception($"Customer With Id {id} Not Found");
            var result =await unitofwork.GenericReposatory<Customer,int>().DeleteAsync(FindCustomer);
            return result;
        }
        public async Task<IEnumerable<CustomerResultDto>> GetAllCustomersAsync()
        {
            var Customers =await unitofwork.GenericReposatory<Customer,int>().GetAllAsync();
            if (Customers == null) throw new Exception("Ther Are Not Any Customer !!");
            var CustomersMapping =mapper.Map<IEnumerable<CustomerResultDto>>(Customers);
            if (CustomersMapping == null) throw new Exception("Mapping Is Not Correct");
            return CustomersMapping;
        }
        public async Task<CustomerResultDto> GetCustomerByIdAsync(int CustomerID)
        {
            var Customer  =await unitofwork.GenericReposatory<Customer,int>().GetByIdAsync(CustomerID);
            if (Customer is null) throw new Exception($"Customer With Id : {CustomerID} Is Not Found");
            var CustomerMapping =mapper.Map<CustomerResultDto>(Customer);
            return CustomerMapping;
        }

    }
}
