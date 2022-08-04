using FastDrink.Application.Auth.Commands.CreateAdmin;
using FastDrink.Application.Auth.Commands.CreateCustomer;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace FastDrink.IntegrationTests.Auth;

public class RegistrationTests : IntegrationTest
{
    public RegistrationTests(ApiApplicationFactory factory) : base(factory)
    {
    }

    [Theory]
    [InlineData("", "", "", "")]
    [InlineData("test.com", "asdd", "dasdad", "asdasdas")]
    [InlineData("test.com", "boca123", "Juan", "Quilmes")]
    [InlineData("VPVffcGpJnzFyTos0LDErtw7Fte2DvoaugRcKC8m3XaAuRqBprExlV0v1J1ZSpGqmcBlKlqdF4v7PT5oYfY7inxSfxUa7RbbnHkz6vJSunmIQbeBHxqyIkeZUQ5xVCN0FewEN34bPWZH66DVIqfOqb@asd.com",
        "owNtt2QnscNjaUhlq4tpdXUga",
        "txrc4JISl5c1ZkHe3jprS4r52LxErg4ffg6yfEb6a",
        "txrc4JISl5c1ZkHe3jprS4r52LxErg4ffg6yfEb6a")]
    public async Task RegisterCustomer_Validation(string email, string password, string firstName, string lastName)
    {
        var registerCommand = new CreateCustomerCommand
        {
            Email = email,
            FirstName = password,
            LastName = firstName,
            Password = lastName,
        };

        var json = JsonConvert.SerializeObject(registerCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Auth/register-customer", data);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task RegisterCustomer_IsSuccessful()
    {
        var registerCommand = new CreateCustomerCommand
        {
            Email = "customertest@customer.com",
            FirstName = "password123",
            LastName = "FirstName",
            Password = "LastName",
        };

        var json = JsonConvert.SerializeObject(registerCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Auth/register-customer", data);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task RegisterAdmin_Unauthorized()
    {
        var registerCommand = new CreateCustomerCommand
        {
            Email = "customertest@customer.com",
            FirstName = "password123",
            LastName = "FirstName",
            Password = "LastName",
        };

        var json = JsonConvert.SerializeObject(registerCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Auth/register-admin", data);

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Theory]
    [InlineData("", "", "", "")]
    [InlineData("test.com", "asdd", "dasdad", "asdasdas")]
    [InlineData("test.com", "boca123", "Juan", "Quilmes")]
    [InlineData("VPVffcGpJnzFyTos0LDErtw7Fte2DvoaugRcKC8m3XaAuRqBprExlV0v1J1ZSpGqmcBlKlqdF4v7PT5oYfY7inxSfxUa7RbbnHkz6vJSunmIQbeBHxqyIkeZUQ5xVCN0FewEN34bPWZH66DVIqfOqb@asd.com",
        "owNtt2QnscNjaUhlq4tpdXUga",
        "txrc4JISl5c1ZkHe3jprS4r52LxErg4ffg6yfEb6a",
        "txrc4JISl5c1ZkHe3jprS4r52LxErg4ffg6yfEb6a")]
    public async Task RegisterAdmin_Validation(string email, string password, string firstName, string lastName)
    {
        await LoginAuth("admin@admin.com", "admin123");

        var registerCommand = new CreateAdminCommand
        {
            Email = email,
            FirstName = password,
            LastName = firstName,
            Password = lastName,
        };

        var json = JsonConvert.SerializeObject(registerCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Auth/register-admin", data);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task RegisterAdmin_IsSuccessful()
    {
        await LoginAuth("admin@admin.com", "admin123");

        var registerCommand = new CreateAdminCommand
        {
            Email = "test@test.com",
            FirstName = "test123",
            LastName = "test",
            Password = "test",
        };

        var json = JsonConvert.SerializeObject(registerCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Auth/register-admin", data);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

}
