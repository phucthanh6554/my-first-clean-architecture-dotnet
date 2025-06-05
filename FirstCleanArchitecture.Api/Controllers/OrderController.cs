using FirstCleanArchitecture.Application.Orders;
using FirstCleanArchitecture.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FirstCleanArchitecture.Api.Controllers;

[Route("api/orders")]
public class OrderController : Controller
{
    private readonly INewOrderUseCase _newOrderUseCase;
    private readonly IListOrderByCustomerUseCase _listOrderByCustomerUseCase;

    public OrderController(INewOrderUseCase newOrderUseCase, IListOrderByCustomerUseCase listOrderByCustomerUseCase)
    {
        _newOrderUseCase = newOrderUseCase;
        _listOrderByCustomerUseCase = listOrderByCustomerUseCase;
    }

    [HttpGet("{customerId:guid}")]
    public async Task<IActionResult> GetOrder(Guid customerId)
    {
        var orders = await _listOrderByCustomerUseCase.ListOrdersAsync(customerId.ToString());
        
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        var result = await _newOrderUseCase.CreateNewOrderAsync(order);
        
        return result ? Ok() : BadRequest();
    }
}