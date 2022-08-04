using System.Net;

namespace FastDrink.IntegrationTests.Product;

public class DeleteProductRouteTests : IntegrationTest
{
    public DeleteProductRouteTests(ApiApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task DeleteSoft_Unauthorized()
    {
        var response = await _client.DeleteAsync("api/Product/EeDJ2yNlzab");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task DeleteSoft_CustomerUnathorized()
    {
        await LoginAuth("customer@customer.com", "customer123");

        var response = await _client.DeleteAsync("api/Product/EeDJ2yNlzab");

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task DeleteSoft_NoExist()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var response = await _client.DeleteAsync("api/Product/EeDJ2yNlzab");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteSoft_IsSuccessful()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var response = await _client.DeleteAsync("api/Product/r4OpzW1xN5B");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task HardDelete_Unathorized()
    {
        var response = await _client.DeleteAsync("api/Product/hard-delete/EeDJ2yNlzab");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task HardDelete_CustomerUnathorized()
    {
        await LoginAuth("customer@customer.com", "customer123");

        var response = await _client.DeleteAsync("api/Product/hard-delete/EeDJ2yNlzab");

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task HardDelete_NoExist()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var response = await _client.DeleteAsync("api/Product/hard-delete/EeDJ2yNlzab");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task HardDelete_IsSuccessful()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var response = await _client.DeleteAsync("api/Product/vD0eJnaz14w");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
