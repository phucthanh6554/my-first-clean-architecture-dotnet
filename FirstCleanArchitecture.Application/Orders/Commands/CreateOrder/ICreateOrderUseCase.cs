using FirstCleanArchitecture.Application.Commons.Results;
using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Orders.Commands.CreateOrder;

public interface ICreateOrderUseCase
{
    Task<GeneralResult<bool>> CreateNewOrderAsync(CreateOrderCommand order);
}