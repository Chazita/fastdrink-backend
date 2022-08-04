using System.Net;

namespace FastDrink.IntegrationTests.Product;

public class ProductAdminRoutesTests : IntegrationTest
{
    public ProductAdminRoutesTests(ApiApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetProductsAdmin_NotAccess()
    {
        var response = await _client.GetAsync($"api/Product/admin");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetProductsAdmin_NotAccessCustomer()
    {
        await LoginAuth("customer@customer.com", "customer123");

        var response = await _client.GetAsync($"api/Product/admin");

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GetProductsAdmin_IsSuccessful()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var response = await _client.GetAsync($"api/Product/admin");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task RecoverProduct_Unathorized()
    {
        var response = await _client.PutAsync("api/Product/recover-product/EeDJ2yNlzab", null);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task RecoverProduct_CustomerUnathorized()
    {
        await LoginAuth("customer@customer.com", "customer123");

        var response = await _client.PutAsync("api/Product/recover-product/EeDJ2yNlzab", null);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task RecoverProduct_NoExist()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var response = await _client.PutAsync("api/Product/recover-product/EeDJ2yNlzab", null);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task RecoverProduct_IsSuccessful()
    {
        await LoginAuth("admin@admin.com", "admin123");

        await _client.DeleteAsync("api/Product/r4OpzW1xN5B");

        var response = await _client.PutAsync("api/Product/recover-product/r4OpzW1xN5B", null);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

}


