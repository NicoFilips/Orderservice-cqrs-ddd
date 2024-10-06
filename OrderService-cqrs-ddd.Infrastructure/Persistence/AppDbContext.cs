using Microsoft.EntityFrameworkCore;
using OrderService_cqrs_ddd.Domain.Aggregates;
using OrderService_cqrs_ddd.Domain.Entities;

namespace OrderService_cqrs_ddd.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {}

    // DbSet für Orders
    public DbSet<Order> Orders { get; set; }

    // DbSet für InventoryItems
    public DbSet<InventoryItem> InventoryItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Konfiguration für Order
        modelBuilder.Entity<Order>().HasKey(o => o.Id);

        // Konfiguration für InventoryItem
        modelBuilder.Entity<InventoryItem>().HasKey(i => i.ProductId);

        // Weitere Konfigurationen falls nötig...
    }
}
