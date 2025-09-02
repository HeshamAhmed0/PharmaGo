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
        public Task<CustomerResultDto> CreateCustomerAsync(string id,CustomerRequestDto customerRequestDto);
        public Task<CustomerResultDto> UpdateCustomerAsync(string Id, CustomerRequestDto customerRequestDto);
        public Task<bool> DeleteCustomerAsync(string id);
        public Task<CustomerResultDto> GetCustomerByIdAsync(string ProductID);
        public Task<IEnumerable<CustomerResultDto>> GetAllCustomersAsync();
    }
}
