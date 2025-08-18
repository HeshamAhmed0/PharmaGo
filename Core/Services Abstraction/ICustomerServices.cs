using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.MedulesDto;

namespace Services_Abstraction
{
    public interface ICustomerServices
    {
        public Task<CustomerResultDto> CreateCustomerAsync(CustomerRequestDto customerRequestDto);
        public Task<CustomerResultDto> UpdateCustomerAsync(int Id, CustomerRequestDto customerRequestDto);
        public Task<bool> DeleteCustomerAsync(int id);
        public Task<CustomerResultDto> GetCustomerByIdAsync(int ProductID);
        public Task<IEnumerable<CustomerResultDto>> GetAllCustomersAsync();
    }
}
