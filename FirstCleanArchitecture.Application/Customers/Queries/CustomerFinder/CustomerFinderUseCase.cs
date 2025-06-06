using System.Net;
using AutoMapper;
using FirstCleanArchitecture.Application.Commons.Results;
using FirstCleanArchitecture.Application.Repository;
using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Customers.Queries.CustomerFinder;

public class CustomerFinderUseCase : ICustomerFinderUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerFinderUseCase(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<GeneralResult<CustomerFinderResponse>> FindCustomerAsync(string customerId)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(customerId);

        if (customer == null)
            return new GeneralResult<CustomerFinderResponse>(HttpStatusCode.NotFound);
        
        var customerResponse = _mapper.Map<CustomerFinderResponse>(customer);

        return new GeneralResult<CustomerFinderResponse>(customerResponse);
    }
}