using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Domain.Entities;

namespace OrderProcessingSystem.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(builder =>
{
    builder.HasKey(o => o.Id);

    builder.Property(o => o.CreatedAt)
        .IsRequired();

    builder.Property(o => o.Status)
        .HasConversion<int>()
        .IsRequired();

    builder.HasMany(o => o.Items)
        .WithOne()
        .HasForeignKey("OrderId");

    builder.Navigation(o => o.Items)
        .UsePropertyAccessMode(PropertyAccessMode.Field);
});

        modelBuilder.Entity<OrderItem>(builder =>
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.ProductName).IsRequired();
            builder.Property(i => i.Price).IsRequired();
            builder.Property(i => i.Quantity).IsRequired();
        });
    }
}