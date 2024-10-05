using OrderService_cqrs_ddd.Application.Commands.Handlers;
using OrderService_cqrs_ddd.Application.Repositories;
using OrderService_cqrs_ddd.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(typeof(CreateOrderCommandHandler).Assembly);

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
