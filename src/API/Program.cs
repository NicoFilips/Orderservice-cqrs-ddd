using FluentValidation;
using MediatR;
using OrderService.API.DependencyInjection;
using OrderService.API.Endpoints.GRPC;
using OrderService.API.Endpoints.MinimalApi;
using OrderService.Application.DependencyInjection;
using OrderService.Application.Validation;
using OrderService.Infrastructure.DependencyInjection;

namespace OrderService.API;

public static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Services configuration
        builder.Services.ConfigureServices();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCustomSwagger();
        builder.Services.AddInfrastructure();
        builder.Services.AddApplication();
        builder.Services.AddGrpc();
        builder.Services.AddGrpcReflection();
        builder.Services.AddControllers();
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderCommandValidator>();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options => { options.InjectStylesheet("/swagger-ui/swagger-dark.css"); });
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

        app.UseAuthorization();
        app.MapControllers();
        app.UseStaticFiles();

        app.Run();
    }
}
