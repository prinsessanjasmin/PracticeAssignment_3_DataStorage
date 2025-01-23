using Business.Models;
using Business.Dtos;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static Customer Create(CustomerDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        return new Customer
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
        };
    }

    public static CustomerEntity Create(Customer customer)
    {
        if (customer == null) throw new ArgumentNullException(nameof(customer));
        return new CustomerEntity
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
        };
    }

    public static Customer Create(CustomerEntity customerEntity)
    {
        if (customerEntity == null) throw new ArgumentNullException(nameof(customerEntity));

        return new Customer
        {
            FirstName = customerEntity.FirstName,
            LastName = customerEntity.LastName,
            Email = customerEntity.Email,
            PhoneNumber = customerEntity.PhoneNumber,
        };
    }
}
