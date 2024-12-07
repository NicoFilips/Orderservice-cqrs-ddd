using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using NUnit.Framework;

namespace OrderService.API.Smoketests;

[TestFixture]
public class StartUpTests
{
    [Test]
    public void Program_WhenCreateHost_ShouldRunWithoutExceptions()
    {
        // Arrange
        string[] args = [];
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Act
        Action buildAppAction = () => builder.Build();

        // Assert
        buildAppAction.Should().NotThrow("because the app should be built without any configuration errors");
    }
}
