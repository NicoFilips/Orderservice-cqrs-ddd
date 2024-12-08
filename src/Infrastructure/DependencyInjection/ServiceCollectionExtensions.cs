using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Repositories;
using OrderService.Infrastructure.Repositories;
using OrderService.SharedKernel.Logging;

namespace OrderService.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.CreateInMemoryContext();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddSerilogLogging();

        return services;
    }
}
