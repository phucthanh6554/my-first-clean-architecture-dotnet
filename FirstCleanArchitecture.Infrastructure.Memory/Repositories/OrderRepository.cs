using FirstCleanArchitecture.Application.Repository;
using FirstCleanArchitecture.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstCleanArchitecture.Infrastructure.Memory.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Order?> GetOrderByIdAsync(string guid)
    {
        var order = await _context.Orders.Where(x => x.Id == guid).FirstOrDefaultAsync();
        
        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerId(string customerId)
    {
        var orders = await _context.Orders.Where(x => x.CustomerId == customerId).ToListAsync();
        
        return orders;
    }
}