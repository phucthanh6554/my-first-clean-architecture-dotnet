using System.Net;
using AutoMapper;
using FirstCleanArchitecture.Application.Orders.Queries.GetListByCustomerId;
using FirstCleanArchitecture.Application.Repository;
using FirstCleanArchitecture.Entities;
using Moq;

namespace FirstCleanArchitecture.Test.Orders.GetListByCustomerId;

public class ListOrderByCustomerUseCaseTest
{
    [Fact]
    public async Task GetListOrder_ValidRequest_Successfully()
    {
        var mockOrderRepository = new Mock<IOrderRepository>();
        var mockCustomerRepository = new Mock<ICustomerRepository>();
        var mockAutomapperRepository = new Mock<IMapper>();
        
        var inputCustomerId = Guid.NewGuid().ToString();

        var expectCustomer = new Customer
        {
            Id = inputCustomerId,
            FirstName = "Mock First name",
            LastName = "Mock Last name",
            Email = "Mock Email",
            DateOfBirth = new DateTime(1997, 9, 15)
        };

        var expectOrders = new List<Order>
        {
            new Order { CustomerId = inputCustomerId, ProductName = "Test Product", Quantity = 1 },
        };
        
        var expectOrdersResponse = new List<OrderListByCustomerIdResponse>
        {
            new OrderListByCustomerIdResponse { CustomerId = inputCustomerId, ProductName = "Test Product", Quantity = 1 },
        };

        mockCustomerRepository.Setup(x => x.GetCustomerByIdAsync(inputCustomerId))
            .ReturnsAsync(expectCustomer);

        mockOrderRepository.Setup(x => x.GetOrdersByCustomerId(inputCustomerId))
            .ReturnsAsync(expectOrders);
        
        mockAutomapperRepository.Setup(x => x.Map<IEnumerable<OrderListByCustomerIdResponse>>(expectOrders))
            .Returns(expectOrdersResponse);
        
        var listOrderUseCase = new ListOrderByCustomerUseCase(mockCustomerRepository.Object, mockOrderRepository.Object, mockAutomapperRepository.Object);
        
        var actualResult = await listOrderUseCase.ListOrdersAsync(inputCustomerId);
        
        Assert.NotNull(actualResult);
        Assert.NotNull(actualResult.ReturnObject);
        Assert.Equal(HttpStatusCode.OK, actualResult.StatusCode);
        Assert.True(actualResult.ReturnObject.Count() > 0);
        Assert.All(actualResult.ReturnObject, order => Assert.Equal(inputCustomerId, order.CustomerId));
        
    }
    
    [Fact]
    public async Task GetListOrder_EmptyCustomerId_ReturnBadRequest()
    {
        var mockOrderRepository = new Mock<IOrderRepository>();
        var mockCustomerRepository = new Mock<ICustomerRepository>();
        var mockAutomapperRepository = new Mock<IMapper>();

        var inputCustomerId = string.Empty;
        
        var listOrderUseCase = new ListOrderByCustomerUseCase(mockCustomerRepository.Object, mockOrderRepository.Object, mockAutomapperRepository.Object);
        
        var actualResult = await listOrderUseCase.ListOrdersAsync(inputCustomerId);
        
        Assert.Equal(HttpStatusCode.BadRequest, actualResult.StatusCode);
        Assert.NotNull(actualResult.ReturnObject);
        Assert.Empty(actualResult.ReturnObject);
        
    }
    
    [Fact]
    public async Task GetListOrder_NotExistsCustomerId_ReturnNotFound()
    {
        var mockOrderRepository = new Mock<IOrderRepository>();
        var mockCustomerRepository = new Mock<ICustomerRepository>();
        var mockAutomapperRepository = new Mock<IMapper>();

        var inputCustomerId = Guid.NewGuid().ToString();

        mockCustomerRepository.Setup(x => x.GetCustomerByIdAsync(inputCustomerId))
            .ReturnsAsync(null as Customer);
        
        var listOrderUseCase = new ListOrderByCustomerUseCase(mockCustomerRepository.Object, mockOrderRepository.Object, mockAutomapperRepository.Object);
        
        var actualResult = await listOrderUseCase.ListOrdersAsync(inputCustomerId);
        
        Assert.Equal(HttpStatusCode.NotFound, actualResult.StatusCode);
        Assert.NotNull(actualResult.ReturnObject);
        Assert.Empty(actualResult.ReturnObject);
    }
}