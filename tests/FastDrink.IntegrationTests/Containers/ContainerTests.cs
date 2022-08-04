using FastDrink.Application.BaseTypes.Commands;
using FastDrink.Domain.Common;
using FastDrink.Domain.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace FastDrink.IntegrationTests.Containers;

public class ContainerTests : IntegrationTest
{
    public ContainerTests(ApiApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetAllContainers_IsSuccessful()
    {
        var response = await _client.GetAsync("api/Container");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.DeserializeContent<IList<BaseType>>();

        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetContainerById_NoExist()
    {
        var response = await _client.GetAsync($"api/Container/{5555}");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseMessage = await response.Content.ReadAsStringAsync();

        responseMessage.Should().NotBeNullOrEmpty();
        responseMessage.Should().Be("No exist Container with 5555 id.");
    }

    [Fact]
    public async Task GetContainerById_IsSuccessful()
    {
        var response = await _client.GetAsync("api/Container/1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseContent = await response.Content.DeserializeContent<BaseType>();

        response.Should().NotBeNull();
        responseContent.Id.Should().Be(1);
        responseContent.Name.Should().NotBeNullOrWhiteSpace().And.Be("botella");
    }

    [Fact]
    public async Task CreateContainer_Unauthorized()
    {
        var createCommand = new CreateBaseTypeCommand<Container>
        {
            Name = "new_brand"
        };

        var json = JsonConvert.SerializeObject(createCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Container", data);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateContainer_CustomerUnauthorized()
    {
        await LoginAuth("customer@customer.com", "customer123");

        var createCommand = new CreateBaseTypeCommand<Container>
        {
            Name = "new_brand"
        };

        var json = JsonConvert.SerializeObject(createCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Container", data);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task CreateContainer_IsSuccessful()
    {
        await LoginAuth("admin@admin.com", "admin123");
        var createCommand = new CreateBaseTypeCommand<Container>
        {
            Name = "new_brand"
        };

        var json = JsonConvert.SerializeObject(createCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Container", data);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task UpdateContainer_Unauthorized()
    {
        var updateCommand = new UpdateBaseTypeCommand<Container>
        {
            Id = 1,
            NewName = "new_name"
        };

        var json = JsonConvert.SerializeObject(updateCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/Container", data);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task UpdateContainer_CustomerUnauthorized()
    {
        await LoginAuth("customer@customer.com", "customer123");

        var updateCommand = new UpdateBaseTypeCommand<Container>
        {
            Id = 1,
            NewName = "new_name"
        };

        var json = JsonConvert.SerializeObject(updateCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/Container", data);

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task UpdateContainer_NoExist()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var updateCommand = new UpdateBaseTypeCommand<Container>
        {
            Id = 5555,
            NewName = "new_name"
        };

        var json = JsonConvert.SerializeObject(updateCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/Container", data);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateContainer_IsSuccessful()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var updateCommand = new UpdateBaseTypeCommand<Container>
        {
            Id = 2,
            NewName = "new_name"
        };

        var json = JsonConvert.SerializeObject(updateCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PutAsync("api/Container", data);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteContainer_Unauthorized()
    {
        var response = await _client.DeleteAsync("api/Container/3");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task DeleteContainer_CustomerUnauthorized()
    {
        await LoginAuth("customer@customer.com", "customer123");

        var response = await _client.DeleteAsync("api/Container/3");

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task DeleteContainer_NoExist()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var response = await _client.DeleteAsync("api/Container/5555");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task DeleteContainer_IsSuccessful()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var response = await _client.DeleteAsync("api/Container/3");

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
