using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstCleanArchitecture.Entities;

public class Order
{
    [Key]
    [Column("Id", TypeName = "varchar(100)")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Column("CustomerId", TypeName = "varchar(100)")]
    public string CustomerId { get; set; } = Guid.NewGuid().ToString();
    
    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; } = null!;
    
    [Column("ProductName", TypeName = "nvarchar(255)")]
    public string ProductName { get; set; } = string.Empty;

    private int _quantity;
    
    [Column("Quantity", TypeName = "int")]
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