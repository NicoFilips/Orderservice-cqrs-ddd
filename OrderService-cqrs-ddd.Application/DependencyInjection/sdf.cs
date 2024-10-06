using Microsoft.Extensions.DependencyInjection;
using OrderService_cqrs_ddd.Application.Commands.CancelOrder;
using OrderService_cqrs_ddd.Application.Commands.CreateOrder;

namespace OrderService_cqrs_ddd.Application.DependencyInjection;

public static class ServiceExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommandHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CancelOrderCommandHandler).Assembly));
    }
}
