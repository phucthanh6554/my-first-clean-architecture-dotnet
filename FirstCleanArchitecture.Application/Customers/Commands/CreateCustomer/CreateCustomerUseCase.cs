using AutoMapper;
using FirstCleanArchitecture.Application.Commons.Results;
using FirstCleanArchitecture.Application.Repository;
using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Customers.Commands.CreateCustomer;


public class CreateCustomerUseCase : ICreateCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CreateCustomerUseCase(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<GeneralResult<bool>> CreateCustomerAsync(CreateCustomerCommand c)
    {
        var customerEntity = _mapper.Map<Customer>(c);
        
        var result = await _customerRepository.CreateCustomerAsync(customerEntity);
        
        return new GeneralResult<bool>(result);
    }
}