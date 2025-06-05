using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Orders;

public interface INewOrderUseCase
{
    Task<bool> CreateNewOrderAsync(Order order);
}