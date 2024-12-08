using Microsoft.EntityFrameworkCore;
using OrderService.Infrastructure.Persistence;
using OrderService.Infrastructure.Util;

namespace OrderService.Infrastructure;

public class AppDbContextFactory
{
    public static AppDbContext CreateInMemoryContext()
    {
        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
                     .UseInMemoryDatabase("TestDatabase")
                     .Options;

        var dbContext = new AppDbContext(options);
        dbContext.SeedData();
        return dbContext;
    }
}
