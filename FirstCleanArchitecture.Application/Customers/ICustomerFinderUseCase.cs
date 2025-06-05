using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Customers;

public interface ICustomerFinderUseCase
{
    Task<Customer?> FindCustomerAsync(string customerId);
}