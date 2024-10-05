using OrderService_cqrs_ddd.API.Filters;
using OrderService_cqrs_ddd.Application.Commands.Handlers;
using OrderService_cqrs_ddd.Application.Repositories;
using OrderService_cqrs_ddd.Infrastructure;

namespace ClassLibrary1OrderService_cqrs_ddd.Application.DependencyInjection;

public void ConfigureServices(IServiceCollection services)
{
    services.AddMediatR(typeof(CreateOrderCommandHandler).Assembly);
    services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddControllers(options =>
        {
            options.Filters.Add<ErrorHandlingFilter>();
        });
}
