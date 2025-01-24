using Business.Dtos;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerEntity Create(CustomerDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        return new CustomerEntity
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
        };
    }

    public static CustomerEntity Create(int id, CustomerDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        return new CustomerEntity
        {
            Id = id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
        };
    }

    public static CustomerDto Create(CustomerEntity customerEntity)
    {
        if (customerEntity == null) throw new ArgumentNullException(nameof(customerEntity));

        return new CustomerDto
        {
            Id = customerEntity.Id,
            FirstName = customerEntity.FirstName,
            LastName = customerEntity.LastName,
            Email = customerEntity.Email,
            PhoneNumber = customerEntity.PhoneNumber,
        };
    }
}
