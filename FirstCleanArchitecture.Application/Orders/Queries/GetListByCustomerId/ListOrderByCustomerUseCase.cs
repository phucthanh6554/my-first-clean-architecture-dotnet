using System.Net;
using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using FirstCleanArchitecture.Application.Commons.Results;
using FirstCleanArchitecture.Application.Repository;
using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Orders.Queries.GetListByCustomerId;

public class ListOrderByCustomerUseCase :IListOrderByCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    
    public ListOrderByCustomerUseCase(ICustomerRepository customerRepository, IOrderRepository orderRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<GeneralResult<IEnumerable<OrderListByCustomerIdResponse>>> ListOrdersAsync(string customerId)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
        
        if(customer == null)
        {
            return new GeneralResult<IEnumerable<OrderListByCustomerIdResponse>>(HttpStatusCode.NotFound,
                "Customer not found", Enumerable.Empty<OrderListByCustomerIdResponse>());
        }
        
        var orders = await _orderRepository.GetOrdersByCustomerId(customer.Id);
        
        var returnOrders = _mapper.Map<IEnumerable<OrderListByCustomerIdResponse>>(orders);
        
        return new GeneralResult<IEnumerable<OrderListByCustomerIdResponse>>(returnOrders);
    }
}