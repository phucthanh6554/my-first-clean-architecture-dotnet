using FirstCleanArchitecture.Application.Repository;
using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Orders;

public class NewOrderUseCase : INewOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;

    public NewOrderUseCase(IOrderRepository orderRepository, ICustomerRepository customerRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
    }

    public async Task<bool> CreateNewOrderAsync(Order order)
    {
        if(order.Quantity < 0)
            return false;

        if (string.IsNullOrEmpty(order.CustomerId))
            return false;
        
        var customer = await _customerRepository.GetCustomerByIdAsync(order.CustomerId);
        
        if(customer == null)
            return false;
        
        return await _orderRepository.CreateOrderAsync(order);
    }
}