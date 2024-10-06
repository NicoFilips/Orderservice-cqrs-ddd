using Microsoft.EntityFrameworkCore;
using OrderService_cqrs_ddd.Infrastructure.Persistence;

namespace OrderService_cqrs_ddd.Infrastructure;

public class AppDbContextFactory
{
    public static AppDbContext CreateInMemoryContext()
    {
        // Konfiguration des InMemory-DbContext
        var options = new DbContextOptionsBuilder<AppDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString())  // Jede Instanz hat eine eigene DB
                     .Options;

        return new AppDbContext(options);
    }
}
