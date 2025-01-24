using Business.Dtos;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface ICustomerService
{
    Task <CustomerEntity> CreateCustomerEntityAsync(CustomerDto dto);
    Task <IEnumerable<CustomerDto>> GetCustomerListAsync();
    Task <CustomerDto> GetCustomerByIdAsync(int id);
    Task <CustomerDto> UpdateCustomerAsync(int id, CustomerDto dto);
    Task <bool> DeleteCustomerAsync(int id);

}
