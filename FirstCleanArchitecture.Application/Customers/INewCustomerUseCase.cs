using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Customers;

public interface INewCustomerUseCase
{
    Task<bool> CreateCustomerAsync(Customer c);
}