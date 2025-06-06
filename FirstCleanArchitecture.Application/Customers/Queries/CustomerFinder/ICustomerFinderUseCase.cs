using FirstCleanArchitecture.Application.Commons.Results;
using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Customers.Queries.CustomerFinder;

public interface ICustomerFinderUseCase
{
    Task<GeneralResult<CustomerFinderResponse>> FindCustomerAsync(string customerId);
}