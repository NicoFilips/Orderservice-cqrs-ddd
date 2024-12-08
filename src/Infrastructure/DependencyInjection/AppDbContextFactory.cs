using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Infrastructure.Persistence;
using OrderService.Infrastructure.Util;

namespace OrderService.Infrastructure.DependencyInjection;

public static class AppDbContextFactory
{
    public static IServiceCollection CreateInMemoryContext(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(
            options =>
                options.UseInMemoryDatabase("InMemoryDb"));

        using ServiceProvider serviceProvider = services.BuildServiceProvider();
        using AppDbContext dbContext = serviceProvider.GetRequiredService<AppDbContext>();

        dbContext.SeedInventoryData();
        dbContext.SeedOrderData();

        return services;
    }
}
