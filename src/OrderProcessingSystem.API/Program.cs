using OrderProcessingSystem.Application.Orders.CreateOrder;
using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Infrastructure.Data;
using OrderProcessingSystem.Application.Orders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ICreateOrderService, CreateOrderService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=orders_db;Username=postgres;Password=postgres"));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();