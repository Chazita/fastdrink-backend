using FastDrink.Domain.Common;
using System.Net;

namespace FastDrink.IntegrationTests.Categories;

public class CategoryTests : IntegrationTest
{
    public CategoryTests(ApiApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task GetAllCategories_IsSuccessful()
    {
        var response = await _client.GetAsync("api/Category");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.DeserializeContent<IList<BaseType>>();

        content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetCategoryById_NoExist()
    {
        var response = await _client.GetAsync($"api/Category/{5555}");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var responseMessage = await response.Content.ReadAsStringAsync();

        responseMessage.Should().NotBeNullOrEmpty();
        responseMessage.Should().Be("No exist Category with 5555 id.");
    }

    [Fact]
    public async Task GetCategoryById_IsSuccessful()
    {
        var response = await _client.GetAsync("api/Category/1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseContent = await response.Content.DeserializeContent<BaseType>();

        response.Should().NotBeNull();
        responseContent.Id.Should().Be(1);
        responseContent.Name.Should().NotBeNullOrWhiteSpace().And.Be("cerveza");
    }
}
