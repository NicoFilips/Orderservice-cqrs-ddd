using OrderService_cqrs_ddd.API.DependencyInjection;
using OrderService_cqrs_ddd.API.Endpoints.GRPC;
using OrderService_cqrs_ddd.API.Endpoints.MinimalAPI;
using OrderService_cqrs_ddd.Application.DependencyInjection;
using OrderService_cqrs_ddd.Infrastructure.DependencyInjection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddGrpc();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// --- gRPC endpoints ---
app.MapGrpcService<OrdersGrpc>();
app.MapGet("/", () => "Use a gRPC client to communicate with the gRPC endpoints. This application does not support HTTP 1.1 requests.");

// --- Minimal API endpoints ---
app.MapOrdersEndpoints();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
