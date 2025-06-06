using System.ComponentModel.DataAnnotations;

namespace FirstCleanArchitecture.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand
{
    [Required]
    public required string CustomerId { get; set; } 
    
    [Required]
    public required string ProductName { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}