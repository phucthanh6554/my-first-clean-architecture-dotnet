using FirstCleanArchitecture.Entities;
using FirstCleanArchitecture.Infrastructure.Memory;
using FirstCleanArchitecture.Infrastructure.Memory.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FirstCleanArchitecture.Infrastructure.Test.Repositories;

public class OrderRepositoryTest
{
    private readonly List<Customer> _sampleCustomer = new List<Customer>
    {
        new (){Id = "9025a0d2-d545-45d3-80bf-466d09e53372", FirstName = "John", LastName = "Doe", Email = "johndoe@gmail.com", DateOfBirth = new DateTime(2000, 8, 17)},
        new (){Id = "ba22452b-e4e3-461a-afa9-0556a85369d3", FirstName = "Phuc", LastName = "Nguyen", Email = "phuc@gmail.com", DateOfBirth = new DateTime(1997, 9, 15)},
        new (){Id = "660eaacd-8009-4793-bcbe-7d265980fa2f", FirstName = "Teo", LastName = "Nguyen", Email = "teo@outlook.com", DateOfBirth = new DateTime(1990, 1, 25)},
    };
    
    private readonly List<Order> _sampleOrders = new()
    {
        new ()
        {
            Id = "c29ba884-8cd5-40f7-b6b0-bc2981c53c13",
            CustomerId = "9025a0d2-d545-45d3-80bf-466d09e53372",
            ProductName = "Tissot Watch",
            Quantity = 1
        },
        new ()
        {
            Id = "92545fd2-058c-43d1-8d4b-adb308d5726e",
            CustomerId = "9025a0d2-d545-45d3-80bf-466d09e53372",
            ProductName = "Bmw Car",
            Quantity = 2
        },
        new ()
        {
            Id = "f9f158de-9970-4cc6-99e7-384469d4484c",
            CustomerId = "660eaacd-8009-4793-bcbe-7d265980fa2f",
            ProductName = "Education Book",
            Quantity = 5
        },
    };

    [Fact]
    public async Task GetOrderById_ValidId_Successfully()
    {
        using var context = CreateDbContext("orders-db-test-1");
        context.Orders.AddRange(_sampleOrders);
        context.SaveChanges();
    
        var orderRepository = new OrderRepository(context);
    
        var expectedOrder = _sampleOrders
            .Where(x => x.Id == "92545fd2-058c-43d1-8d4b-adb308d5726e")
            .First();
        
        var actualOrder = await orderRepository.GetOrderByIdAsync(expectedOrder.Id);
        
        Assert.NotNull(actualOrder);
        Assert.Equal(expectedOrder.Id, actualOrder.Id);
        Assert.Equal(expectedOrder.CustomerId, actualOrder.CustomerId);
    }
    
    [Fact]
    public async Task GetOrderById_NotExistsOrder_ReturnNotFound()
    {
        using var context = CreateDbContext("orders-db-test-2");
        context.Orders.AddRange(_sampleOrders);
        context.SaveChanges();
    
        var orderRepository = new OrderRepository(context);
    
        var notExistsOrderId = "becb01b0-965e-4eab-b5d6-599b5dac7862";
        
        var actualOrder = await orderRepository.GetOrderByIdAsync(notExistsOrderId);
        
        Assert.Null(actualOrder);
    }
    
    [Fact]
    public async Task GetOrderByCustomerId_ValidId_Successfully()
    {
        using var context = CreateDbContext("orders-db-test-3");
        
        context.Customers.AddRange(_sampleCustomer);
        context.SaveChanges();
        
        context.Orders.AddRange(_sampleOrders);
        context.SaveChanges();

        var orderRepository = new OrderRepository(context);

        var expectedCustomerId = "9025a0d2-d545-45d3-80bf-466d09e53372";
        
        var actualOrders = (await orderRepository.GetOrdersByCustomerId(expectedCustomerId)).ToList();
        
        Assert.NotNull(actualOrders);
        Assert.NotEmpty(actualOrders);
        Assert.All(actualOrders, order => Assert.Equal(expectedCustomerId, order.CustomerId));
    }

    private ApplicationDbContext CreateDbContext(string dbName)
    {
        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;
        
        var context = new ApplicationDbContext(dbContextOptions);
        
        return context;
    }
}