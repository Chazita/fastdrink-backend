using FastDrink.Application.Products.Commands.UpdateProduct;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace FastDrink.IntegrationTests.Product;

public class UpdateProductRouteTests : IntegrationTest
{
    public UpdateProductRouteTests(ApiApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task UpdateProduct_Unauthorized()
    {
        var updateCommand = new UpdateProductCommand()
        {
            BrandId = 1,
            CategoryId = 1,
            ContainerId = 1,
            Discount = 5,
            Id = "r4OpzW1xN5B",
            Name = "test",
            Price = 123,
            Stock = 100,
            Volume = 100
        };

        var json = JsonConvert.SerializeObject(updateCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/Product", data);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UpdateProduct_CustomerUnathorized()
    {
        await LoginAuth("customer@customer.com", "customer123");

        var updateCommand = new UpdateProductCommand()
        {
            BrandId = 1,
            CategoryId = 1,
            ContainerId = 1,
            Discount = 5,
            Id = "r4OpzW1xN5B",
            Name = "test",
            Price = 123,
            Stock = 100,
            Volume = 100
        };

        var json = JsonConvert.SerializeObject(updateCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/Product", data);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UpdateProduct_IsSuccessful()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var updateCommand = new UpdateProductCommand()
        {
            BrandId = 1,
            CategoryId = 1,
            ContainerId = 1,
            Discount = 5,
            Id = "r4OpzW1xN5B",
            Name = "test",
            Price = 123,
            Stock = 100,
            Volume = 100
        };

        var json = JsonConvert.SerializeObject(updateCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/Product", data);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task UpdateProduct_NoExist()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var updateCommand = new UpdateProductCommand()
        {
            BrandId = 1,
            CategoryId = 1,
            ContainerId = 1,
            Discount = 5,
            Id = "EeDJ2yNlzab",
            Name = "test",
            Price = 123,
            Stock = 100,
            Volume = 100
        };

        var json = JsonConvert.SerializeObject(updateCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/Product", data);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
