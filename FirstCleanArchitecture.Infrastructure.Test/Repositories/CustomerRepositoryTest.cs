using System.Data.SqlTypes;
using FirstCleanArchitecture.Entities;
using FirstCleanArchitecture.Infrastructure.Memory;
using FirstCleanArchitecture.Infrastructure.Memory.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FirstCleanArchitecture.Infrastructure.Test.Repositories;

public class CustomerRepositoryTest
{
    private readonly List<Customer> _sampleCustomer = new List<Customer>
    {
        new (){Id = "9025a0d2-d545-45d3-80bf-466d09e53372", FirstName = "John", LastName = "Doe", Email = "johndoe@gmail.com", DateOfBirth = new DateTime(2000, 8, 17)},
        new (){Id = "ba22452b-e4e3-461a-afa9-0556a85369d3", FirstName = "Phuc", LastName = "Nguyen", Email = "phuc@gmail.com", DateOfBirth = new DateTime(1997, 9, 15)},
        new (){Id = "660eaacd-8009-4793-bcbe-7d265980fa2f", FirstName = "Teo", LastName = "Nguyen", Email = "teo@outlook.com", DateOfBirth = new DateTime(1990, 1, 25)},
    };

    [Fact]
    public async Task CreateCustomer_ValidRequest_Successfully()
    {
        var context = CreateDbContext("testing-db-1");
        
        var customerRepository = new CustomerRepository(context);

        var sampleCreateCustomer = new Customer
        {
            FirstName = "Mock First Name",
            LastName = "Mock Last Name",
            Email = "mock@gmail.com",
            DateOfBirth = new DateTime(2000, 4, 1)
        };

        var actualResult = await customerRepository.CreateCustomerAsync(sampleCreateCustomer);
        
        Assert.True(actualResult);

        context.Database.EnsureDeleted();
    }
    
    [Fact]
    public async Task CreateCustomer_DuplicateId_ThrowsException()
    {
        var context = CreateDbContext("testing-db-2");
        
        context.Customers.AddRange(_sampleCustomer);
        context.SaveChanges();
        
        var customerRepository = new CustomerRepository(context);

        var sampleCreateCustomer = new Customer
        {
            Id = _sampleCustomer.First().Id,
            FirstName = "Mock First Name",
            LastName = "Mock Last Name",
            Email = "mock@gmail.com",
            DateOfBirth = new DateTime(2000, 4, 1)
        };
        
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await customerRepository.CreateCustomerAsync(sampleCreateCustomer));
    }
    
    [Fact]
    public async Task GetCustomerById_ExistsCustomerId_Successfully()
    {
        var context = CreateDbContext("testing-db-3");
        context.Customers.AddRange(_sampleCustomer);
        context.SaveChanges();
        
        var customerRepository = new CustomerRepository(context);

        var expectedCustomer = _sampleCustomer
            .Where(x => x.Id == "ba22452b-e4e3-461a-afa9-0556a85369d3")
            .First();

        var actualResult = await customerRepository.GetCustomerByIdAsync(expectedCustomer.Id);
        
        Assert.NotNull(actualResult);
        Assert.Equal(expectedCustomer.Id, actualResult.Id);
        Assert.Equal(expectedCustomer.Email, actualResult.Email);
    }
    
    [Fact]
    public async Task GetCustomerById_NonExistsCustomerId_ReturnNotFound()
    {
        var context = CreateDbContext("testing-db-4");
        context.Customers.AddRange(_sampleCustomer);
        context.SaveChanges();
        
        var customerRepository = new CustomerRepository(context);

        var notExistsCustomerId = "5a4ddee8-743b-456e-9b29-6040acf2f3d3";

        var actualResult = await customerRepository.GetCustomerByIdAsync(notExistsCustomerId);
        
        Assert.Null(actualResult);
    }

    private ApplicationDbContext CreateDbContext(string dbName)
    {
        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        
        var context = new ApplicationDbContext(dbContextOptions);
        
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();   
        
        return context;
    }
}