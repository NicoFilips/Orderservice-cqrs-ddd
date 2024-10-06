using OrderService_cqrs_ddd.API.Filters;
using OrderService_cqrs_ddd.Application.Commands.CreateOrder;
using OrderService_cqrs_ddd.Application.Repositories;
using OrderService_cqrs_ddd.Infrastructure;
using OrderService_cqrs_ddd.Infrastructure.Repositories;
using OrderService_cqrs_ddd.Shared.Logging;

namespace OrderService_cqrs_ddd.API.DependencyInjection;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        // Registriere MediatR, Repositories, Logging und andere Dienste
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommandHandler).Assembly));
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddSerilogLogging();

        services.AddControllers(options =>
        {
            options.Filters.Add<ErrorHandlingFilter>();
        });
    }
}
