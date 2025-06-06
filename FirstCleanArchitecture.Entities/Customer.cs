using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstCleanArchitecture.Entities;

public class Customer
{
    [Key]
    [Column("Id", TypeName = "varchar(100)")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [Column("FirstName", TypeName = "varchar(100)")]
    public string FirstName { get; set; } = string.Empty;
    
    [Column("LastName", TypeName = "varchar(100)")]
    public string LastName { get; set; } = string.Empty;
    
    [Column("Email", TypeName = "varchar(255)")]
    public string Email { get; set; } = string.Empty;
    
    [Column("DateOfBirth", TypeName = "datetime")]
    public DateTime DateOfBirth { get; set; }
    
    public virtual IEnumerable<Order>? Orders { get; set; } 
}