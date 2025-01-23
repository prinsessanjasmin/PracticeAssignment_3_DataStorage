
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
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

    public Customer CreateCustomer(CustomerDto dto)
    {
        try
        {
            Customer customer = CustomerFactory.Create(dto);
            return customer;
        }
        catch (Exception ex) 
        {
            Debug.WriteLine($"Error creating customer. :: {ex.Message}");
            return null!;
        }

    }

    public async Task <CustomerEntity> CreateCustomerEntityAsync(Customer customer)
    {
        try
        {
            CustomerEntity customerEntity = CustomerFactory.Create(customer);
            await _customerRepository.CreateAsync(customerEntity); 
            return customerEntity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating customer entity. :: {ex.Message}");
            return null!;
        }
    }

    public async Task <IEnumerable<Customer>> GetCustomerListAsync()
    {
        try
        {
            var entityList = await _customerRepository.GetAllAsync();
            List<Customer> result = new List<Customer>();
            foreach (var entity in entityList)
            {
                Customer customer = CustomerFactory.Create(entity);
                result.Add(customer);
            }
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching customer list. :: {ex.Message}");
            return null!;
        }
    }


    public async Task <Customer> GetCustomerByIdAsync(int id)
    {
        if (id < 0)
            return null!;

        try
        {
            var customerEntity = await _customerRepository.GetCustomerByIdAsync(id);
            Customer customer = CustomerFactory.Create(customerEntity);
            return customer;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error fetching customer. :: {ex.Message}");
            return null!; 
        }
    }


    public async Task <Customer> UpdateCustomerAsync(Customer customer)
    {
        try
        {
            CustomerEntity customerEntity = CustomerFactory.Create(customer);
            CustomerEntity updatedContact = await _customerRepository.UpdateAsync(customerEntity); 
            
            return CustomerFactory.Create(customerEntity);
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
