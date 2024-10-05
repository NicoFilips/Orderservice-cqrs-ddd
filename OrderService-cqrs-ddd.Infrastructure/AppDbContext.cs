using Microsoft.EntityFrameworkCore;

namespace OrderService_cqrs_ddd.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
}
