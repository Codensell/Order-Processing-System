using OrderProcessingSystem.Application.Orders.CreateOrder;
using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Infrastructure.Data;
using OrderProcessingSystem.Application.Orders;
using OrderProcessingSystem.Application.Orders.GetOrderById;
using OrderProcessingSystem.Application.Orders.GetOrders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ICreateOrderService, CreateOrderService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=orders_db;Username=postgres;Password=postgres"));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IGetOrderByIdService, GetOrderByIdService>();
builder.Services.AddScoped<IGetOrdersService, GetOrdersService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();