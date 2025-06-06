using FirstCleanArchitecture.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstCleanArchitecture.Infrastructure.Memory;

public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Order> Orders { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
}