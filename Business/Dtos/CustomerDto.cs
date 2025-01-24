using Business.Services;

namespace Business.Dtos;

public class CustomerDto
{
    public int? Id { get; set; } 
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public CustomerDto(string firstName, string lastName, string email, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public CustomerDto() { }

    public override string ToString()
    {
        string customer = ($"#: {Id}\nName: {FirstName} {LastName}\nEmail: <{Email}> \nPhone number:{PhoneNumber}");
        return customer;
    }
}
