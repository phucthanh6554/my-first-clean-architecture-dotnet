using System.Net;
using FirstCleanArchitecture.Application.Orders;
using FirstCleanArchitecture.Application.Orders.Commands.CreateOrder;
using FirstCleanArchitecture.Application.Orders.Queries;
using FirstCleanArchitecture.Application.Orders.Queries.GetListByCustomerId;
using FirstCleanArchitecture.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FirstCleanArchitecture.Api.Controllers;

[Route("api/orders")]
public class OrderController : Controller
{
    private readonly ICreateOrderUseCase _createOrderUseCase;
    private readonly IListOrderByCustomerUseCase _listOrderByCustomerUseCase;

    public OrderController(ICreateOrderUseCase createOrderUseCase, IListOrderByCustomerUseCase listOrderByCustomerUseCase)
    {
        _createOrderUseCase = createOrderUseCase;
        _listOrderByCustomerUseCase = listOrderByCustomerUseCase;
    }

    [HttpGet("{customerId:guid}")]
    public async Task<IActionResult> GetOrder(Guid customerId)
    {
        var orders = await _listOrderByCustomerUseCase.ListOrdersAsync(customerId.ToString());
        
        if(orders.StatusCode == HttpStatusCode.NotFound)
            return NotFound(orders.Message);
        
        return Ok(orders.ReturnObject);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand order)
    {
        var result = await _createOrderUseCase.CreateNewOrderAsync(order);
        
        if(result.StatusCode == HttpStatusCode.NotFound)
            return NotFound(result.Message);
        
        if(result.StatusCode == HttpStatusCode.BadRequest)
            return BadRequest(result.Message);
        
        return Ok(result.ReturnObject);  
    }
}