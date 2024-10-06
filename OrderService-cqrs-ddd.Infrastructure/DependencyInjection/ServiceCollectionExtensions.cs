using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderService_cqrs_ddd.Application.Repositories;
using OrderService_cqrs_ddd.Infrastructure.Persistence;
using OrderService_cqrs_ddd.Infrastructure.Repositories;

namespace OrderService_cqrs_ddd.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("InMemoryDb"));
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();

        return services;
    }
}
