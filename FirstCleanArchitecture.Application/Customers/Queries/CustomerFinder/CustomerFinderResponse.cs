namespace FirstCleanArchitecture.Application.Customers.Queries.CustomerFinder;

public class CustomerFinderResponse
{
    public string Id { get; set; } = string.Empty;  
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public DateTime DateOfBirth { get; set; }
}