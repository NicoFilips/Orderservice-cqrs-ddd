using Microsoft.EntityFrameworkCore;
using OrderService_cqrs_ddd.Infrastructure.Persistence;

namespace OrderService_cqrs_ddd.Infrastructure;

public class AppDbContextFactory
{
    public static AppDbContext CreateInMemoryContext()
    {
        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString())
                     .Options;

        return new AppDbContext(options);
    }
}
