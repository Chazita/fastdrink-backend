using FastDrink.Application.Common.Models;
using FastDrink.Application.Products.DTOs;
using System.Net;

namespace FastDrink.IntegrationTests.Product;

public class ProductCustomerRoutesTests : IntegrationTest
{
    public ProductCustomerRoutesTests(ApiApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetProducts_IsSuccessful()
    {
        var response = await _client.GetAsync($"api/Product");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.DeserializeContent<PaginatedList<ProductDto>>();

        content.Should().NotBeNull();
        content.Items.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetProductsDetails_NotExist()
    {

        var response = await _client.GetAsync($"api/Product/get-details/EeDJ2yNlzab");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.DeserializeContent<IDictionary<string, string>>();

        content.Values.Should().NotBeNull();
        content.Values.Should().HaveCount(1);

        content.Keys.Should().Contain("Producto");
        content.Values.Should().Contain("El product con el ID: 9999 no existe.");
    }

    [Fact]
    public async Task GetProductsDetails_IsSuccessful()
    {

        var response = await _client.GetAsync($"api/Product/get-details/r4OpzW1xN5B");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
