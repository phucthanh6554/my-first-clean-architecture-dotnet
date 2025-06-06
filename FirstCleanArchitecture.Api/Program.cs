

using FirstCleanArchitecture.Application.Commons.Mappings;
using FirstCleanArchitecture.Application.Customers;
using FirstCleanArchitecture.Application.Customers.Commands.CreateCustomer;
using FirstCleanArchitecture.Application.Customers.Queries.CustomerFinder;
using FirstCleanArchitecture.Application.Orders;
using FirstCleanArchitecture.Application.Orders.Commands.CreateOrder;
using FirstCleanArchitecture.Application.Orders.Queries;
using FirstCleanArchitecture.Application.Orders.Queries.GetListByCustomerId;
using FirstCleanArchitecture.Application.Repository;
using FirstCleanArchitecture.Infrastructure.Memory;
using FirstCleanArchitecture.Infrastructure.Memory.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(OrderMappingProfile).Assembly);

builder.Services.AddDbContext<ApplicationDbContext>(c =>
{
    c.UseInMemoryDatabase("InMemoryDatabase");
});

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<IListOrderByCustomerUseCase, ListOrderByCustomerUseCase>();
builder.Services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();

builder.Services.AddScoped<ICustomerFinderUseCase, CustomerFinderUseCase>();
builder.Services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}