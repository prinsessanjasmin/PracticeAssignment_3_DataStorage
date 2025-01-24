using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : ICustomerRepository
{
    private readonly DataContext _context = context;

    public async Task<CustomerEntity> CreateAsync(CustomerEntity customer)
    {
        if (customer == null)
            return null!;

        try
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer; 
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating customer entity. :: {ex.Message}");
            return null!; 
        }
    }



    public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {

        try
        {
            return await _context.Customers.ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating customer entity. :: {ex.Message}");
            return null!;
        }
    }

    public async Task<CustomerEntity> GetCustomerByIdAsync(int id)
    {
        if (id < 0)
            return null!;

        try
        {
            return await _context.Customers.FirstOrDefaultAsync(x => x.Id == id) ?? null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating customer entity. :: {ex.Message}");
            return null!;
        }
    }

    public async Task<CustomerEntity> UpdateAsync(CustomerEntity updatedCustomer)
    {
        if (updatedCustomer == null)
            return null!;

        try
        {
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == updatedCustomer.Id);
            if (existingCustomer == null)
                return null!; 

            _context.Entry(existingCustomer).CurrentValues.SetValues(updatedCustomer);
            await _context.SaveChangesAsync();
            return existingCustomer!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating customer entity. :: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        if (id < 0)
            return false;

        try
        {
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCustomer == null)
                return false; 

            _context.Customers.Remove(existingCustomer);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating customer entity. :: {ex.Message}");
            return false;
        }

    }
}
