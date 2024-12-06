using OrderService.API.Filters;
using OrderService.SharedKernel.Logging;

namespace OrderService.API.DependencyInjection;

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
