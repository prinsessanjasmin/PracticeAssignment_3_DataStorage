using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.ConsoleApp.Dialogue;

var serviceCollection = new ServiceCollection();
serviceCollection.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFileName = C:\Projects\PracticeAssignment_3\Data\Database\CustomerDatabase.mdf"));
serviceCollection.AddScoped<ICustomerService, CustomerService>();
serviceCollection.AddScoped<ICustomerRepository, CustomerRepository>();
serviceCollection.AddScoped<CustomerDialogue>();

var serviceProvider  = serviceCollection.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var customerDialogue = scope.ServiceProvider.GetRequiredService<CustomerDialogue>();
    await customerDialogue.MainMenuAsync();
}

