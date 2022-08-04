using System.Net;

namespace FastDrink.IntegrationTests.Product;

public class CreateProductRouteTests : IntegrationTest
{
    public CreateProductRouteTests(ApiApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CreateProduct_Unauthorized()
    {
        var file = CreateFormFile();
        using var multipartFormContent = new MultipartFormDataContent()
        {
            {new StreamContent(file),"Photo","test.txt"},
            {new StringContent("1"), "BrandId"},
            {new StringContent("1"), "CategoryId"},
            {new StringContent("1"), "ContainerId"},
            {new StringContent("test"), "Name"},
            {new StringContent("123"), "Price"},
            {new StringContent("100"), "Stock"},
            {new StringContent("100"), "Volume"},
        };

        var response = await _client.PostAsync("api/Product", multipartFormContent);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateProduct_CustomerUnauthorized()
    {
        await LoginAuth("customer@customer.com", "customer123");

        var file = CreateFormFile();
        using var multipartFormContent = new MultipartFormDataContent()
        {
            {new StreamContent(file),"Photo","test.txt"},
            {new StringContent("1"), "BrandId"},
            {new StringContent("1"), "CategoryId"},
            {new StringContent("1"), "ContainerId"},
            {new StringContent("test"), "Name"},
            {new StringContent("123"), "Price"},
            {new StringContent("100"), "Stock"},
            {new StringContent("100"), "Volume"},
        };

        var response = await _client.PostAsync("api/Product", multipartFormContent);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task CreateProduct_IsSuccessful()
    {
        await LoginAuth("admin@admin.com", "admin123");
        var file = CreateFormFile();
        using var multipartFormContent = new MultipartFormDataContent()
        {
            {new StreamContent(file),"Photo","test.txt"},
            {new StringContent("1"), "BrandId"},
            {new StringContent("1"), "CategoryId"},
            {new StringContent("1"), "ContainerId"},
            {new StringContent("test"), "Name"},
            {new StringContent("123"), "Price"},
            {new StringContent("100"), "Stock"},
            {new StringContent("100"), "Volume"},
        };

        var response = await _client.PostAsync("api/Product", multipartFormContent);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [InlineData("", "", "", "", "", "", "")]
    [InlineData("-1", "-1", "-1", "-1", "-1", "-1", "123456789101112131415161718192021222324252627282930123456789101112131415161718192021222324252627282930")]
    [InlineData("a", "b", "c", "d", "f", "g", "123456789101112131415161718192021222324252627282930123456789101282930")]
    public async Task CreateProduct_Validation(string brandId, string categoryId, string containerId, string price, string stock, string volume, string name)
    {
        await LoginAuth("admin@admin.com", "admin123");

        var file = CreateFormFile();

        using var multipartFormContent = new MultipartFormDataContent()
        {
            {new StreamContent(file),"Photo","test.txt"},
            {new StringContent(brandId), "BrandId"},
            {new StringContent(categoryId), "CategoryId"},
            {new StringContent(containerId), "ContainerId"},
            {new StringContent(name), "Name"},
            {new StringContent(price), "Price"},
            {new StringContent(stock), "Stock"},
            {new StringContent(volume), "Volume"},
        };

        var response = await _client.PostAsync("api/Product", multipartFormContent);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateProduct_NoFile()
    {
        await LoginAuth("admin@admin.com", "admin123");

        using var multipartFormContent = new MultipartFormDataContent()
        {
            {new StringContent("1"), "BrandId"},
            {new StringContent("1"), "CategoryId"},
            {new StringContent("1"), "ContainerId"},
            {new StringContent("test"), "Name"},
            {new StringContent("123"), "Price"},
            {new StringContent("100"), "Stock"},
            {new StringContent("100"), "Volume"},
        };

        var response = await _client.PostAsync("api/Product", multipartFormContent);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
