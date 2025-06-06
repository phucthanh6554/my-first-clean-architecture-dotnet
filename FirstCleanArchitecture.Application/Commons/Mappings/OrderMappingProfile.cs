using AutoMapper;
using FirstCleanArchitecture.Application.Orders.Commands.CreateOrder;
using FirstCleanArchitecture.Application.Orders.Queries.GetListByCustomerId;
using FirstCleanArchitecture.Entities;

namespace FirstCleanArchitecture.Application.Commons.Mappings;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<CreateOrderCommand, Order>();
        CreateMap<Order, OrderListByCustomerIdResponse>();
    }
}