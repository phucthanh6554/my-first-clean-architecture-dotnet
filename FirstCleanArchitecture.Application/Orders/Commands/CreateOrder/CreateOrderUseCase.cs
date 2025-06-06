using System.Net;
using AutoMapper;
using FirstCleanArchitecture.Application.Commons.Results;
using FirstCleanArchitecture.Application.Repository;
using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Orders.Commands.CreateOrder;

public class CreateOrderUseCase : ICreateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CreateOrderUseCase(IOrderRepository orderRepository, ICustomerRepository customerRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<GeneralResult<bool>> CreateNewOrderAsync(CreateOrderCommand order)
    {
        if (order.Quantity < 1)
            return new GeneralResult<bool>(HttpStatusCode.BadRequest, "Invalid quantity", false);

        if (string.IsNullOrEmpty(order.CustomerId))
            return new GeneralResult<bool>(HttpStatusCode.BadRequest, "Invalid Customer Id", false);
        
        var customer = await _customerRepository.GetCustomerByIdAsync(order.CustomerId);
        
        if(customer == null)
            return new GeneralResult<bool>(HttpStatusCode.NotFound, "Customer is not found", false);
        
        var orderEntity = _mapper.Map<Order>(order);
        
        var result = await _orderRepository.CreateOrderAsync(orderEntity);

        return new GeneralResult<bool>(result);
    }
}