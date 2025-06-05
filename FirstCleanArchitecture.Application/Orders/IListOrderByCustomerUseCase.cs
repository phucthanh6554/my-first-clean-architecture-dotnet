using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Orders;

public interface IListOrderByCustomerUseCase
{
    Task<IEnumerable<Order>> ListOrdersAsync(string customerId);
}