using FastDrink.Application.BaseTypes.Commands;
using FastDrink.Domain.Common;
using FastDrink.Domain.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace FastDrink.IntegrationTests.Brands;

public class BrandTests : IntegrationTest
{
    public BrandTests(ApiApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetAllBrands_IsSuccessful()
    {
        var response = await _client.GetAsync("api/Brand");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.DeserializeContent<IList<BaseType>>();

        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetBrandById_NoExist()
    {
        var response = await _client.GetAsync($"api/Brand/{5555}");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseMessage = await response.Content.ReadAsStringAsync();

        responseMessage.Should().NotBeNullOrEmpty();
        responseMessage.Should().Be("No exist Brand with 5555 id.");
    }

    [Fact]
    public async Task GetBrandById_IsSuccessful()
    {
        var response = await _client.GetAsync("api/Brand/1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseContent = await response.Content.DeserializeContent<BaseType>();

        response.Should().NotBeNull();
        responseContent.Id.Should().Be(1);
        responseContent.Name.Should().NotBeNullOrWhiteSpace().And.Be("andes_origen");
    }

    [Fact]
    public async Task CreateBrand_Unauthorized()
    {
        var createCommand = new CreateBaseTypeCommand<Brand>
        {
            Name = "new_brand"
        };

        var json = JsonConvert.SerializeObject(createCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Brand", data);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateBrand_CustomerUnauthorized()
    {
        await LoginAuth("customer@customer.com", "customer123");

        var createCommand = new CreateBaseTypeCommand<Brand>
        {
            Name = "new_brand"
        };

        var json = JsonConvert.SerializeObject(createCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Brand", data);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task CreateBrand_IsSuccessful()
    {
        await LoginAuth("admin@admin.com", "admin123");
        var createCommand = new CreateBaseTypeCommand<Brand>
        {
            Name = "new_brand"
        };

        var json = JsonConvert.SerializeObject(createCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Brand", data);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task UpdateBrand_Unauthorized()
    {
        var updateCommand = new UpdateBaseTypeCommand<Brand>
        {
            Id = 1,
            NewName = "new_name"
        };

        var json = JsonConvert.SerializeObject(updateCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/Brand", data);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UpdateBrand_CustomerUnauthorized()
    {
        await LoginAuth("customer@customer.com", "customer123");

        var updateCommand = new UpdateBaseTypeCommand<Brand>
        {
            Id = 1,
            NewName = "new_name"
        };

        var json = JsonConvert.SerializeObject(updateCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/Brand", data);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UpdateBrand_NoExist()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var updateCommand = new UpdateBaseTypeCommand<Brand>
        {
            Id = 5555,
            NewName = "new_name"
        };

        var json = JsonConvert.SerializeObject(updateCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/Brand", data);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateBrand_IsSuccessful()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var updateCommand = new UpdateBaseTypeCommand<Brand>
        {
            Id = 2,
            NewName = "new_name"
        };

        var json = JsonConvert.SerializeObject(updateCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/Brand", data);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteBrand_Unauthorized()
    {
        var response = await _client.DeleteAsync("api/Brand/3");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task DeleteBrand_CustomerUnauthorized()
    {
        await LoginAuth("customer@customer.com", "customer123");

        var response = await _client.DeleteAsync("api/Brand/3");

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task DeleteBrand_NoExist()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var response = await _client.DeleteAsync("api/Brand/5555");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteBrand_IsSuccessful()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var response = await _client.DeleteAsync("api/Brand/3");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
