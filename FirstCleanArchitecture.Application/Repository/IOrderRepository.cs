using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Repository;

public interface IOrderRepository
{
    Task<bool> CreateOrderAsync(Order order);
    
    Task<Order?> GetOrderByIdAsync(string guid);

    Task<IEnumerable<Order>> GetOrdersByCustomerId(string customerId);
}