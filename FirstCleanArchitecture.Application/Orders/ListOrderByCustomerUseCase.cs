using FirstCleanArchitecture.Application.Repository;
using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Orders;

public class ListOrderByCustomerUseCase :IListOrderByCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;

    public ListOrderByCustomerUseCase(ICustomerRepository customerRepository, IOrderRepository orderRepository)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Order>> ListOrdersAsync(string customerId)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
        
        if(customer == null)
            return Enumerable.Empty<Order>();
        
        return await _orderRepository.GetOrdersByCustomerId(customer.Id);
    }
}