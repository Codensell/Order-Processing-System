using OrderProcessingSystem.Application.Orders.CreateOrder;
using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Infrastructure.Data;
using OrderProcessingSystem.Application.Orders;
using OrderProcessingSystem.Application.Orders.GetOrderById;
using OrderProcessingSystem.Application.Orders.GetOrders;
using OrderProcessingSystem.API.Middleware;
using FluentValidation;
using OrderProcessingSystem.Application.Orders.CreateOrder.Validators;
using OrderProcessingSystem.Application.Orders.Events;
using OrderProcessingSystem.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<KafkaOptions>(
    builder.Configuration.GetSection("Kafka"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderRequestValidator>();

builder.Services.AddScoped<ICreateOrderService, CreateOrderService>();
builder.Services.AddScoped<IGetOrderByIdService, GetOrderByIdService>();
builder.Services.AddScoped<IGetOrdersService, GetOrdersService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<IOrderEventPublisher, KafkaOrderEventPublisher>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();