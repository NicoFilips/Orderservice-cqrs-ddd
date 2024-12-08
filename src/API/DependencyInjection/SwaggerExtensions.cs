using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using OrderService.Application.Commands.CreateOrder;

namespace OrderService.API.DependencyInjection;

public static class SwaggerExtensions
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Order API",
                Version = "v1",
                Description = "API for managing orders"
            });

            // Beispielwerte für `CreateOrderRequest` definieren
            c.MapType<CreateOrderCommand>(() => new OpenApiSchema
            {
                Type = "object",
                Properties = new Dictionary<string, OpenApiSchema>
                {
                    ["customerId"] = new OpenApiSchema
                    {
                        Type = "string",
                        Example = new OpenApiString("123e4567-e89b-12d3-a456-426614174000")
                    },
                    ["productId"] = new OpenApiSchema
                    {
                        Type = "string",
                        Example = new OpenApiString("product-001")
                    },
                    ["quantity"] = new OpenApiSchema
                    {
                        Type = "integer",
                        Example = new OpenApiInteger(10)
                    }
                }
            });

            // Optionale Einstellungen: XML-Kommentare oder Sicherheit
            // c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "YourApi.xml"));
        });

        return services;
    }
}
