using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Repository;

public interface ICustomerRepository
{
    Task<bool> CreateCustomerAsync(Customer customer);
    
    Task<Customer?> GetCustomerByIdAsync(string guid);
}