using System.Net;
using FirstCleanArchitecture.Application.Customers.Commands.CreateCustomer;
using FirstCleanArchitecture.Application.Customers.Queries.CustomerFinder;
using Microsoft.AspNetCore.Mvc;

namespace FirstCleanArchitecture.Api.Controllers;

[Route("api/customers")]
public class CustomerController : Controller
{
    private readonly ICustomerFinderUseCase _customerFinderUseCase;
    private readonly ICreateCustomerUseCase _createCustomerUseCase;

    public CustomerController(ICustomerFinderUseCase customerFinderUseCase, ICreateCustomerUseCase createCustomerUseCase)
    {
        _customerFinderUseCase = customerFinderUseCase;
        _createCustomerUseCase = createCustomerUseCase;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCustomerById([FromRoute] Guid id)
    {
        var result = await _customerFinderUseCase.FindCustomerAsync(id.ToString());
        
        if(result.StatusCode == HttpStatusCode.NotFound)
            return NotFound();
        
        return Ok(result.ReturnObject);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand customer)
    {
        var result = await _createCustomerUseCase.CreateCustomerAsync(customer);

        if (result.StatusCode == HttpStatusCode.OK && result.ReturnObject)
            return Ok();

        return BadRequest(result);
    }
}