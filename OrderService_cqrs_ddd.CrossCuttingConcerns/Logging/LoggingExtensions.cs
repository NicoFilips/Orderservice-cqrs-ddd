using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace OrderService_cqrs_ddd.Shared.Logging;

public static class LoggingExtensions
{
    public static IServiceCollection AddSerilogLogging(this IServiceCollection services)
    {
        // Konfiguration für Serilog
        Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Setzt die Mindest-Log-Stufe für Microsoft-Komponenten
                    .MinimumLevel.Information()  // Setzt die Standard-Log-Stufe
                    .Enrich.FromLogContext()  // Fügt standardmäßige Enrichment-Felder hinzu (wie RequestId)
                    .WriteTo.Console()        // Schreibt Logs in die Konsole
                    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // Schreibt Logs in eine Datei, rollierend pro Tag
                    .CreateLogger();

        // Fügt Serilog als Logging-Anbieter hinzu
        services.AddLogging(loggingBuilder =>
            loggingBuilder.AddSerilog(dispose: true));

        return services;
    }
}
