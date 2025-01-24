
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;


    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task <CustomerEntity> CreateCustomerEntityAsync(CustomerDto dto)
    {
        try
        {
            CustomerEntity customerEntity = CustomerFactory.Create(dto);
            await _customerRepository.CreateAsync(customerEntity); 
            return customerEntity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating customer entity. :: {ex.Message}");
            return null!;
        }
    }

    public async Task <IEnumerable<CustomerDto>> GetCustomerListAsync()
    {
        try
        {
            var entityList = await _customerRepository.GetAllAsync();
            List<CustomerDto> result = [];
            foreach (var entity in entityList)
            {
                CustomerDto dto = CustomerFactory.Create(entity);
                result.Add(dto);
            }
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching customer list. :: {ex.Message}");
            return null!;
        }
    }


    public async Task <CustomerDto> GetCustomerByIdAsync(int id)
    {
        if (id < 0)
            return null!;

        try
        {
            var customerEntity = await _customerRepository.GetCustomerByIdAsync(id);
            CustomerDto dto = CustomerFactory.Create(customerEntity);
            return dto;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching customer. :: {ex.Message}");
            return null!; 
        }
    }


    public async Task <CustomerDto> UpdateCustomerAsync(int id, CustomerDto dto)
    {
        try
        {
            CustomerEntity editedEntity = CustomerFactory.Create(id, dto);
            CustomerEntity existingEntity = await _customerRepository.UpdateAsync(editedEntity); 
            
            return CustomerFactory.Create(existingEntity);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating customer. :: {ex.Message}");
            return null!;
        }
    }

    public async Task <bool> DeleteCustomerAsync(int id)
    {
        if (id < 0)
            return false!;

        try
        {
            bool result = await _customerRepository.DeleteAsync(id);
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting customer. :: {ex.Message}");
            return false;
        }
    }
}
