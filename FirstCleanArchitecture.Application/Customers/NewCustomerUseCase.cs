using FirstCleanArchitecture.Application.Repository;
using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Customers;


public class NewCustomerUseCase : INewCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;

    public NewCustomerUseCase(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<bool> CreateCustomerAsync(Customer c)
    {
        return await _customerRepository.CreateCustomerAsync(c);
    }
}