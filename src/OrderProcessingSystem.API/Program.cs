using OrderProcessingSystem.Application.Orders.CreateOrder;
using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Infrastructure.Data;
using OrderProcessingSystem.Application.Orders;
using OrderProcessingSystem.Application.Orders.GetOrderById;
using OrderProcessingSystem.Application.Orders.GetOrders;
using OrderProcessingSystem.API.Middleware;
using FluentValidation;
using OrderProcessingSystem.Application.Orders.CreateOrder.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ICreateOrderService, CreateOrderService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=orders_db;Username=postgres;Password=postgres"));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IGetOrderByIdService, GetOrderByIdService>();
builder.Services.AddScoped<IGetOrdersService, GetOrdersService>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderRequestValidator>();

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();