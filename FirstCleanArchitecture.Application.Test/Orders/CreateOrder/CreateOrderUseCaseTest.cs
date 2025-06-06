using System.Net;
using AutoMapper;
using FirstCleanArchitecture.Application.Orders.Commands.CreateOrder;
using FirstCleanArchitecture.Application.Repository;
using FirstCleanArchitecture.Entities;
using Moq;

namespace FirstCleanArchitecture.Test.Orders.CreateOrder;

public class CreateOrderUseCaseTest
{
    [Fact]
    public async Task CreateNewOrder_ValidRequest_Successfully()
    {
        var mockOrderRepository = new Mock<IOrderRepository>();
        var mockCustomerRepository = new Mock<ICustomerRepository>();
        var mockAutomapperRepository = new Mock<IMapper>();

        var createOrderCommand = new CreateOrderCommand
        {
            CustomerId = Guid.NewGuid().ToString(),
            ProductName = "Test Product",
            Quantity = 1
        };

        var expectedOrderEntity = new Order
        {
            CustomerId = createOrderCommand.CustomerId,
            ProductName = createOrderCommand.ProductName,
            Quantity = createOrderCommand.Quantity
        };

        mockCustomerRepository.Setup(c => c.GetCustomerByIdAsync(createOrderCommand.CustomerId))
            .ReturnsAsync(new Customer{ Id = createOrderCommand.CustomerId });

        mockAutomapperRepository.Setup(c => c.Map<Order>(createOrderCommand))
            .Returns(expectedOrderEntity);

        mockOrderRepository.Setup(x => x.CreateOrderAsync(expectedOrderEntity))
            .ReturnsAsync(true);
        
        var createOrderUseCase = new CreateOrderUseCase(mockOrderRepository.Object, 
            mockCustomerRepository.Object, 
            mockAutomapperRepository.Object);
        
        var createResult = await createOrderUseCase.CreateNewOrderAsync(createOrderCommand);
        
        Assert.Equal(HttpStatusCode.OK, createResult.StatusCode);
        Assert.True(createResult.ReturnObject);
    }
    
    [Fact]
    public async Task CreateNewOrder_InvalidQuantity_ReturnBadRequest()
    {
        var mockOrderRepository = new Mock<IOrderRepository>();
        var mockCustomerRepository = new Mock<ICustomerRepository>();
        var mockAutomapperRepository = new Mock<IMapper>();

        var createOrderCommand = new CreateOrderCommand
        {
            CustomerId = Guid.NewGuid().ToString(),
            ProductName = "Test Product",
            Quantity = 0
        };

        var expectedOrderEntity = new Order
        {
            CustomerId = createOrderCommand.CustomerId,
            ProductName = createOrderCommand.ProductName,
            Quantity = createOrderCommand.Quantity
        };
        
        var createOrderUseCase = new CreateOrderUseCase(mockOrderRepository.Object, 
            mockCustomerRepository.Object, 
            mockAutomapperRepository.Object);
        
        var createResult = await createOrderUseCase.CreateNewOrderAsync(createOrderCommand);
        
        Assert.Equal(HttpStatusCode.BadRequest, createResult.StatusCode);
        Assert.False(createResult.ReturnObject);
    }
    
    [Fact]
    public async Task CreateNewOrder_EmptyCustomerId_ReturnBadRequest()
    {
        var mockOrderRepository = new Mock<IOrderRepository>();
        var mockCustomerRepository = new Mock<ICustomerRepository>();
        var mockAutomapperRepository = new Mock<IMapper>();

        var createOrderCommand = new CreateOrderCommand
        {
            CustomerId = string.Empty,
            ProductName = "Test Product",
            Quantity = 1
        };

        var expectedOrderEntity = new Order
        {
            CustomerId = createOrderCommand.CustomerId,
            ProductName = createOrderCommand.ProductName,
            Quantity = createOrderCommand.Quantity
        };
        
        var createOrderUseCase = new CreateOrderUseCase(mockOrderRepository.Object, 
            mockCustomerRepository.Object, 
            mockAutomapperRepository.Object);
        
        var createResult = await createOrderUseCase.CreateNewOrderAsync(createOrderCommand);
        
        Assert.Equal(HttpStatusCode.BadRequest, createResult.StatusCode);
        Assert.False(createResult.ReturnObject);
    }
    
    [Fact]
    public async Task CreateNewOrder_NonExistsCustomerId_ReturnNotFound()
    {
        var mockOrderRepository = new Mock<IOrderRepository>();
        var mockCustomerRepository = new Mock<ICustomerRepository>();
        var mockAutomapperRepository = new Mock<IMapper>();

        var createOrderCommand = new CreateOrderCommand
        {
            CustomerId = Guid.NewGuid().ToString(),
            ProductName = "Test Product",
            Quantity = 1
        };

        var expectedOrderEntity = new Order
        {
            CustomerId = createOrderCommand.CustomerId,
            ProductName = createOrderCommand.ProductName,
            Quantity = createOrderCommand.Quantity
        };

        mockCustomerRepository.Setup(c => c.GetCustomerByIdAsync(createOrderCommand.CustomerId))
            .ReturnsAsync(null as Customer);
        
        var createOrderUseCase = new CreateOrderUseCase(mockOrderRepository.Object, 
            mockCustomerRepository.Object, 
            mockAutomapperRepository.Object);
        
        var createResult = await createOrderUseCase.CreateNewOrderAsync(createOrderCommand);
        
        Assert.Equal(HttpStatusCode.NotFound, createResult.StatusCode);
        Assert.False(createResult.ReturnObject);
    }
}