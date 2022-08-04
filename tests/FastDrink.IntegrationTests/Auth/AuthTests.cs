using FastDrink.Application.Auth.DTOs;
using System.Net;

namespace FastDrink.IntegrationTests.Auth;

public class AuthTests : IntegrationTest
{
    public AuthTests(ApiApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CheckUser_Unauthorized()
    {
        var response = await _client.GetAsync("api/Auth/check-user");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CheckUser_IsSuccessful()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var response = await _client.GetAsync("api/Auth/check-user");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Should().NotBeNull();

        var content = await response.Content.DeserializeContent<UserDto>();

        content.Email.Should().MatchRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        content.FirstName.Should().NotBeEmpty();
        content.LastName.Should().NotBeEmpty();
        content.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task CheckAdmin_Unauthorized()
    {
        var response = await _client.GetAsync("api/Auth/check-admin");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CheckAdmin_IsSuccessful()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var response = await _client.GetAsync("api/Auth/check-admin");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Should().NotBeNull();

        var content = await response.Content.DeserializeContent<UserDto>();

        content.Email.Should().MatchRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        content.FirstName.Should().NotBeEmpty();
        content.LastName.Should().NotBeEmpty();
        content.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task CheckAdmin_CustomerUnauthorized()
    {
        await LoginAuth("customer@customer.com", "customer123");

        var response = await _client.GetAsync("api/Auth/check-admin");

        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}
