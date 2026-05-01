using OrderProcessingSystem.Application.Orders.CreateOrder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ICreateOrderService, CreateOrderService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();