using FirstCleanArchitecture.Application.Repository;
using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Customers;

public class CustomerFinderUseCase : ICustomerFinderUseCase
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerFinderUseCase(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Customer?> FindCustomerAsync(string customerId)
    {
        return await _customerRepository.GetCustomerByIdAsync(customerId);
    }
}