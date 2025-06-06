using FirstCleanArchitecture.Application.Commons.Results;
using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Customers.Commands.CreateCustomer;

public interface ICreateCustomerUseCase
{
    Task<GeneralResult<bool>> CreateCustomerAsync(CreateCustomerCommand c);
}