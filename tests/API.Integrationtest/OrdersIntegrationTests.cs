using System.Net.Http.Json;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using NUnit.Framework;
using OrderService.Application.Commands.CreateOrder;
using OrderService.Infrastructure.Persistence;

namespace API.Integrationtest;
[TestFixture]
[Ignore("This is a manual test and should only be run manually.")]
public class OrdersIntegrationTests
{
#pragma warning disable IDE0044
    private HttpClient _client;
    private Mock<IMediator> _mediatorMock;
#pragma warning restore IDE0044

    [SetUp]
    public void SetUp()
    {
        _mediatorMock = new Mock<IMediator>();

        WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>()
           .WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureServices(
                        services =>
                        {
                            // Füge eine In-Memory-Datenbank hinzu
                            services.AddDbContext<AppDbContext>(
                                options =>
                                    options.UseInMemoryDatabase("TestDb"));

                            // Ersetze IMediator durch den Mock
                            services.AddSingleton(_mediatorMock.Object);
                        });
                });

        _client = factory.CreateClient();
    }

    [TearDown]
    public void TearDown() => _client.Dispose();

    [Test]
    [Category("Manual")]
    public async Task CreateOrder_Should_Return_OrderId()
    {
        // Arrange: Setup für den Mock-Mediator
        var fakeOrderId = Guid.NewGuid();
        _mediatorMock
           .Setup(m => m.Send(It.IsAny<CreateOrderCommand>(), default))
           .ReturnsAsync(fakeOrderId);

        var request = new
        {
            customerId = Guid.NewGuid(),
            item = new
            {
                productId = Guid.NewGuid(),
                quantity = 2,
            },
        };

        // Act
        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/orders", request);

        // Assert
        response.EnsureSuccessStatusCode(); // 200-Statuscode erwartet
        Guid orderId = await response.Content.ReadFromJsonAsync<Guid>();

        // FluentAssertions
        orderId.Should().Be(fakeOrderId);
    }

    [Test]
    [Category("Manual")]
    public async Task CreateOrder_Should_Return_BadRequest_If_Invalid_Data()
    {
        // Arrange
        var invalidRequest = new
        {
            customerId = Guid.Empty, // Ungültige GUID
            item = new
            {
                productId = Guid.Empty,
                quantity = -1 // Ungültige Menge
            }
        };

        // Act
        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/orders", invalidRequest);

        // Assert: Überprüfe, ob ein 400-Statuscode zurückgegeben wird
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
}
