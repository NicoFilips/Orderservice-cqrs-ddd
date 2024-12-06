using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace OrderService_cqrs_ddd.SharedKernel.Logging;

public static class LoggingExtensions
{
    public static IServiceCollection AddSerilogLogging(this IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext()
                    .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
                    .CreateLogger();

        services.AddLogging(loggingBuilder =>
            loggingBuilder.AddSerilog(dispose: true));

        return services;
    }
}
