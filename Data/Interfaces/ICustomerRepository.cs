using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface ICustomerRepository
{
    Task <CustomerEntity> CreateAsync (CustomerEntity customer);
    Task <IEnumerable<CustomerEntity>> GetAllAsync ();
    Task <CustomerEntity> GetCustomerByIdAsync (int id);
    Task <CustomerEntity> UpdateAsync (CustomerEntity updatedCustomer);
    Task<bool> DeleteAsync(int id); 

}
