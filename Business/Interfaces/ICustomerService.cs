using Business.Dtos;
using Business.Models;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Interfaces;

public interface ICustomerService
{
    public Customer CreateCustomer(CustomerDto dto);
    Task <CustomerEntity> CreateCustomerEntityAsync(Customer customer);
    Task <IEnumerable<Customer>> GetCustomerListAsync();
    Task <Customer> GetCustomerByIdAsync(int id);
    Task <Customer> UpdateCustomerAsync(Customer customer);
    Task <bool> DeleteCustomerAsync(int id);

}
