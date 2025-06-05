using FirstCleanArchitecture.Application.Repository;
using FirstCleanArchitecture.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstCleanArchitecture.Infrastructure.Memory.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _context;

    public CustomerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateCustomerAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Customer?> GetCustomerByIdAsync(string guid)
    {
        var customer = await _context.Customers.Where(x => x.Id == guid).FirstOrDefaultAsync();
        
        return customer;
    }
}