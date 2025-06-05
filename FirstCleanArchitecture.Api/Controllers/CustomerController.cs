using FirstCleanArchitecture.Application.Customers;
using FirstCleanArchitecture.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FirstCleanArchitecture.Api.Controllers;

[Route("api/customers")]
public class CustomerController : Controller
{
    private readonly ICustomerFinderUseCase _customerFinderUseCase;
    private readonly INewCustomerUseCase _newCustomerUseCase;

    public CustomerController(ICustomerFinderUseCase customerFinderUseCase, INewCustomerUseCase newCustomerUseCase)
    {
        _customerFinderUseCase = customerFinderUseCase;
        _newCustomerUseCase = newCustomerUseCase;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCustomerById([FromRoute] Guid id)
    {
        var customer = await _customerFinderUseCase.FindCustomerAsync(id.ToString());
        
        return customer != null ? Ok(customer) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
    {
        var result = await _newCustomerUseCase.CreateCustomerAsync(customer);
        
        return result ? Ok(result) : BadRequest();
    }
}