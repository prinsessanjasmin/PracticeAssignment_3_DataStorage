using Business.Dtos;
using Business.Interfaces;
using Data.Entities;
using Business.Factories;
using System.Collections.Generic;

namespace Presentation.ConsoleApp.Dialogue;

public class CustomerDialogue(ICustomerService customerService)
{
    private readonly ICustomerService customerService = customerService;

    public async Task MainMenuAsync()
    {
        string choice;
        bool exit = false;

        do
        {
            Console.Clear();
            Console.WriteLine("------ Welcome to the Customer Database ------");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Create new customer");
            Console.WriteLine("2. View all customers");
            Console.WriteLine("3. View customer details");
            Console.WriteLine("4. Update customer");
            Console.WriteLine("5. Delete customer");
            Console.WriteLine("6. Exit");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Make your choice: ");
            choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    await CreateNewCustomer();
                    Pause();
                    break;

                case "2":
                    await ViewAllCustomers();
                    Pause();
                    break;

                case "3":
                    await GetCustomerById();
                    Pause();
                    break;

                case "4":
                    await UpdateCustomer();
                    Pause();
                    break;

                case "5":
                    await DeleteCustomer();
                    Pause();
                    break;

                case "6":
                    exit = ExitApp(exit);
                    break;

                default:
                    Console.WriteLine("You must make a choice!");
                    Pause();
                    break;
            }
        } while (!exit);

        Console.WriteLine("Thanks for using the Customer Database. Have a good day!");
        Console.ReadKey();
    }


    public async Task CreateNewCustomer()
    {
        Console.Clear();
        Console.WriteLine("----- CREATE NEW CUSTOMER -----");
        Console.Write("First name: ");
        string firstName = Console.ReadLine()!;
        Console.Write("Last name: ");
        string lastName = Console.ReadLine()!;
        Console.Write("Email: ");
        string email = Console.ReadLine()!;
        Console.Write("Phone number: ");
        string phoneNumber = Console.ReadLine()!;


        CustomerDto dto = new(firstName, lastName, email, phoneNumber);

        CustomerEntity entity = await customerService.CreateCustomerEntityAsync(dto);

        Console.WriteLine("Success!");
        Console.WriteLine("The customer you added: ");
        Console.WriteLine(entity.Id);
        Console.WriteLine($"{entity.FirstName} {entity.LastName}");
        Console.WriteLine(entity.Email);
        Console.WriteLine(entity.PhoneNumber);
        Console.ReadKey();
    }

    public async Task ViewAllCustomers()
    {
        var list = await customerService.GetCustomerListAsync();
        Console.Clear();
        Console.WriteLine("----- VIEW CUSTOMERS -----");
        Console.WriteLine("Your customers: ");
        foreach (var customer in list)
        {
            Console.WriteLine(customer.ToString());
            Console.WriteLine();
        }
    }


    public async Task GetCustomerById()
    {
        var list = await customerService.GetCustomerListAsync();
        Console.Clear();
        Console.WriteLine("----- VIEW CUSTOMER DETAILS -----");
        Console.WriteLine("Here is a list of the customers.");
        foreach (var item in list)
        {
            Console.WriteLine($"Id: {item.Id}. Name: {item.FirstName} {item.LastName}");
        }
        Console.WriteLine();
        Console.WriteLine("Enter the id of the one you would like to see.");
        string idString = Console.ReadLine()!;
        int id = int.Parse(idString);
        var customer = await customerService.GetCustomerByIdAsync(id);
        Console.WriteLine(customer.ToString());
    }

    public async Task UpdateCustomer()
    {
        var list = await customerService.GetCustomerListAsync();
        Console.Clear();
        Console.WriteLine("----- UPDATE CUSTOMER -----");
        Console.WriteLine("Here is a list of the customers.");
        foreach (var item in list)
        {
            Console.WriteLine($"Id: {item.Id}. Name: {item.FirstName} {item.LastName}");
        }
        Console.WriteLine();
        Console.WriteLine("Enter the id of the one you would like to update.");
        string idString = Console.ReadLine()!;
        int id = int.Parse(idString);

        var existingCustomer = await customerService.GetCustomerByIdAsync(id);
        Console.Clear();
        Console.WriteLine(existingCustomer.ToString());

        Console.WriteLine("Which field would you like to update?");
        Console.WriteLine("1. First name");
        Console.WriteLine("2. Last name");
        Console.WriteLine("3. Email");
        Console.WriteLine("4. Phone number");
        Console.Write("Choose a number: ");
        string number = Console.ReadLine()!;

        switch (number)
        {
            case "1":
                Console.Write("Enter the first name: ");
                string firstName = Console.ReadLine()!;
                CustomerDto editedCustomer = new(firstName, existingCustomer.LastName, existingCustomer.Email, existingCustomer.PhoneNumber);
                CustomerDto updatedCustomer = await customerService.UpdateCustomerAsync(id, editedCustomer);
                Console.WriteLine();
                Console.WriteLine("Success!");
                Console.WriteLine("Updated customer: ");
                Console.WriteLine(updatedCustomer.ToString());
                break;

            case "2":
                Console.Write("Enter the last name: ");
                string lastName = Console.ReadLine()!;
                editedCustomer = new(existingCustomer.FirstName, lastName, existingCustomer.Email, existingCustomer.PhoneNumber);
                updatedCustomer = await customerService.UpdateCustomerAsync(id, editedCustomer);
                Console.WriteLine();
                Console.WriteLine("Success!");
                Console.WriteLine("Updated customer: ");
                Console.WriteLine(updatedCustomer.ToString());
                break;

            case "3":
                Console.Write("Enter the email address: ");
                string email = Console.ReadLine()!;
                editedCustomer = new(existingCustomer.FirstName, existingCustomer.LastName, email, existingCustomer.PhoneNumber);
                updatedCustomer = await customerService.UpdateCustomerAsync(id, editedCustomer);
                Console.WriteLine();
                Console.WriteLine("Success!");
                Console.WriteLine("Updated customer: ");
                Console.WriteLine(updatedCustomer.ToString());
                break;

            case "4":
                Console.Write("Enter the phone number: ");
                string phoneNumber = Console.ReadLine()!;
                editedCustomer = new(existingCustomer.FirstName, existingCustomer.LastName, existingCustomer.Email, phoneNumber);
                updatedCustomer = await customerService.UpdateCustomerAsync(id, editedCustomer);
                Console.WriteLine();
                Console.WriteLine("Success!");
                Console.WriteLine("Updated customer: ");
                Console.WriteLine(updatedCustomer.ToString());
                break;

            default:
                Console.WriteLine("You didn't make a valid choice. Please try again. ");
                break;
        }
    }

    public async Task DeleteCustomer()
    {
        var list = await customerService.GetCustomerListAsync();
        Console.Clear();
        Console.WriteLine("----- DELETE CUSTOMER -----");
        Console.WriteLine("Here is a list of the customers.");
        foreach (var item in list)
        {
            Console.WriteLine($"Id: {item.Id}. Name: {item.FirstName} {item.LastName}");
        }
        Console.WriteLine();
        Console.WriteLine("Enter the id of the customer you wish to delete!");
        string idString = Console.ReadLine()!;
        int id = int.Parse(idString);
        bool result = await customerService.DeleteCustomerAsync(id);
        if (result)
        {
            Console.WriteLine("Success!");
        }
        else
        {
            Console.WriteLine("Something went wrong...");
        }
    }

    public void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("Press any key to return to the main menu");
        Console.ReadKey();
    }

    public bool ExitApp(bool exit)
    {
        Console.Write("Are you sure you want to exit the application? Y/N: ");
        string confirm = Console.ReadLine()!;
        if (confirm.Equals("y", StringComparison.OrdinalIgnoreCase))
        {
            exit = true;
        }
        else if (confirm.Equals("n", StringComparison.OrdinalIgnoreCase))
        {
            Pause();
        }
        else
        {
            Console.Write("You didn't make a valid choice.");
            Pause();
        }
        return exit;
    }
}
