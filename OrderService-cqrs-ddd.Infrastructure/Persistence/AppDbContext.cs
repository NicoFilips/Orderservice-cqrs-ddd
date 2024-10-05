using Microsoft.EntityFrameworkCore;
using OrderService_cqrs_ddd.Domain.Entities;

namespace OrderService_cqrs_ddd.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<InventoryItem> InventoryItems { get; set; }
}
