using OrderService.API.DependencyInjection;
using OrderService.API.Endpoints.GRPC;
using OrderService.API.Endpoints.MinimalApi;
using OrderService.Application.DependencyInjection;
using OrderService.Infrastructure.DependencyInjection;

namespace OrderService.API;

public static class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Services configuration
        builder.Services.ConfigureServices();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddInfrastructure();
        builder.Services.AddApplication();
        builder.Services.AddGrpc();
        builder.Services.AddGrpcReflection();
        builder.Services.AddControllers();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // gRPC Endpoints
        app.UseGrpcWeb();
        app.MapGrpcService<OrdersGrpc>();

        app.MapGet("/grpc-info", () => "Use a gRPC client to communicate with the gRPC endpoints.")
           .WithTags("gRPC")
           .WithSummary("Information about gRPC endpoints")
           .WithDescription("gRPC Endpoints")
           .WithName("GrpcInfoEndpoint")
           .WithOpenApi();

        // Minimal API Endpoints
        app.MapOrdersEndpoints();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
