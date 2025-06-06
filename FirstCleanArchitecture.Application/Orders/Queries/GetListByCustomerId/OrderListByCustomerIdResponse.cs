namespace FirstCleanArchitecture.Application.Orders.Queries.GetListByCustomerId;

public class OrderListByCustomerIdResponse
{
    public string Id { get; set; } = string.Empty;
    
    public string CustomerId { get; set; } = string.Empty;
    
    public string ProductName { get; set; } = string.Empty;
    
    public int Quantity { get; set; } 
}