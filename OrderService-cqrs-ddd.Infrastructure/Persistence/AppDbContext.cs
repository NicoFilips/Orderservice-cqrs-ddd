using Microsoft.EntityFrameworkCore;
using OrderService_cqrs_ddd.Domain.Aggregates;
using OrderService_cqrs_ddd.Domain.Entities;

namespace OrderService_cqrs_ddd.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {}

    public DbSet<Order> Orders { get; set; }

    public DbSet<InventoryItem> InventoryItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>().HasKey(o => o.Id);

        modelBuilder.Entity<InventoryItem>().HasKey(i => i.ProductId);
    }
}
