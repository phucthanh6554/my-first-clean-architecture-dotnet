using AutoMapper;
using FirstCleanArchitecture.Application.Customers.Commands.CreateCustomer;
using FirstCleanArchitecture.Application.Customers.Queries.CustomerFinder;
using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Commons.Mappings;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<Customer, CustomerFinderResponse>();
    }
}