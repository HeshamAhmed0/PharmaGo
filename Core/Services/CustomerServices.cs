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
        public async Task<CustomerResultDto> CreateCustomerAsync(string id,CustomerRequestDto customerRequestDto)
        {
            if (customerRequestDto is null)  throw new Exception($"Customer Request Not Found !!");
            var customer =new Customer()
            {
                Id = id,
                Email = customerRequestDto.Email,
                Name = customerRequestDto.Name,
                City = customerRequestDto.City,
                Country = customerRequestDto.Country,
                Street = customerRequestDto.Street,
                PhoneNumber = customerRequestDto.PhoneNumber,
            };
            if (customer is null) throw new Exception($"Customer Mapping Is Not Correct");
            await unitofwork.GenericReposatory<Customer,string>().AddAsync(customer);
            //var CustomerResult =mapper.Map<CustomerResultDto>(customer);
            var CustomerResult = new CustomerResultDto()
            {
                Email = customerRequestDto.Email,
                Name = customerRequestDto.Name,
                City = customerRequestDto.City,
                Country = customerRequestDto.Country,
                Street = customerRequestDto.Street,
                PhoneNumber = customerRequestDto.PhoneNumber,
            };
            return CustomerResult;
        }
        public async Task<CustomerResultDto> UpdateCustomerAsync(string Id, CustomerRequestDto customerRequestDto)
        {
            var customer =await unitofwork.GenericReposatory<Customer,string>().GetByIdAsync(Id);
            if (customer is null) throw new Exception($"User With Id : {Id} Is Not Found");
            var Customermapping =mapper.Map(customerRequestDto, customer);
            await unitofwork.GenericReposatory<Customer, string>().UpadateAsync(Customermapping);
            var result = await unitofwork.GenericReposatory<Customer, string>().GetByIdAsync(Id);
            var mappingForResultDto =mapper.Map<CustomerResultDto>(result);
            return mappingForResultDto;
        }
        public async Task<bool> DeleteCustomerAsync(string id)
        {
           var FindCustomer =await unitofwork.GenericReposatory<Customer,string>().GetByIdAsync(id);
            if (FindCustomer is null) throw new Exception($"Customer With Id {id} Not Found");
            var result =await unitofwork.GenericReposatory<Customer,string>().DeleteAsync(FindCustomer);
            return result;
        }
        public async Task<IEnumerable<CustomerResultDto>> GetAllCustomersAsync()
        {
            var Customers =await unitofwork.GenericReposatory<Customer,string>().GetAllAsync();
            if (Customers == null) throw new Exception("Ther Are Not Any Customer !!");
            var CustomersMapping =mapper.Map<IEnumerable<CustomerResultDto>>(Customers);
            if (CustomersMapping == null) throw new Exception("Mapping Is Not Correct");
            return CustomersMapping;
        }
        public async Task<CustomerResultDto> GetCustomerByIdAsync(string CustomerID)
        {
            var Customer  =await unitofwork.GenericReposatory<Customer,string>().GetByIdAsync(CustomerID);
            if (Customer is null) throw new Exception($"Customer With Id : {CustomerID} Is Not Found");
            var CustomerMapping =mapper.Map<CustomerResultDto>(Customer);
            return CustomerMapping;
        }

    }
}
