using FirstCleanArchitecture.Application.Commons.Results;
using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Orders.Queries.GetListByCustomerId;

public interface IListOrderByCustomerUseCase
{
    Task<GeneralResult<IEnumerable<OrderListByCustomerIdResponse>>> ListOrdersAsync(string customerId);
}