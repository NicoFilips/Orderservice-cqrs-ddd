using Microsoft.EntityFrameworkCore;
using OrderService_cqrs_ddd.API.Filters;
using OrderService_cqrs_ddd.Infrastructure.Repositories;
using OrderService_cqrs_ddd.Shared.Logging;

namespace OrderService_cqrs_ddd.API.DependencyInjection;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddSerilogLogging();

        services.AddControllers(options =>
        {
            options.Filters.Add<ErrorHandlingFilter>();
        });
    }
}
