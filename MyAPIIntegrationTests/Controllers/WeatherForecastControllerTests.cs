using FluentAssertions;
using MyAPI;
using MyAPI.Models;
using MyAPIIntegrationTests.Fixtures;
using Newtonsoft.Json;

namespace MyAPIIntegrationTests.Controllers;

public class WeatherForecastControllerTests
    : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public WeatherForecastControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    [Trait("Category", "Functional")]
    public async Task Last5Days_ReturnsCorrectAmount()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("WeatherForecast/Last5Days");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299

        //  FluentActions.Invoking(() => { }).Should().Throw<InvalidOperationException>();

        var responseData = await response.Content.ReadAsStringAsync();
        var parsed = JsonConvert.DeserializeObject<List<WeatherForecast>>(responseData);
        parsed.Should().HaveCount(5);
    }

    [Fact]
    [Trait("Category", "Functional")]
    public async Task Total_ReturnsOverrideTestValue()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/WeatherForecast/Total");

        // Assert
        response.EnsureSuccessStatusCode();

        var responseData = await response.Content.ReadAsStringAsync();
        responseData.Should().Be("22");
    }
}
