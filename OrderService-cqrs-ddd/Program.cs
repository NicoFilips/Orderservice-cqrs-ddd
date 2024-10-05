using OrderService_cqrs_ddd.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// MediatR für CQRS
builder.Services.AddMediatR(typeof(CreateOrderCommandHandler).Assembly);

// Dependency Injection für Repository und andere Services
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Controller hinzufügen
builder.Services.AddControllers();

// Weitere Middleware wie Swagger hinzufügen (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
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