using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Commands.CancelOrder;
using OrderService.Application.Commands.CreateOrder;

namespace OrderService.Application.DependencyInjection;

public static class ServiceExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommandHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CancelOrderCommandHandler).Assembly));
    }
}
