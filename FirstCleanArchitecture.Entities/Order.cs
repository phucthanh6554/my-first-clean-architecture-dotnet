using System.ComponentModel.DataAnnotations.Schema;

namespace FirstCleanArchitecture.Entities;

public class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public string CustomerId { get; set; } = Guid.NewGuid().ToString();
    
    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; } = null!;
    
    public string ProductName { get; set; } = string.Empty;

    private int _quantity;
    public int Quantity
    {
        get => _quantity;

        set
        {
            if(value < 0)
                throw new ArgumentException("Quantity cannot be negative");
            
            _quantity = value;
        }
    }
}