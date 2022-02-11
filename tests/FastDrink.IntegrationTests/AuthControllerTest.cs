using FastDrink.Application.Auth.Commands.CreateAdmin;
using FastDrink.Application.Auth.Commands.CreateCustomer;
using FastDrink.Application.Auth.Commands.Login;
using FastDrink.Application.Common.Models;
using FastDrink.IntegrationTests.Fixtures;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FastDrink.IntegrationTests;

public class AuthControllerTest : IntegrationTest
{
    private const string AuthRootUri = "/api/Auth";
    public AuthControllerTest(ApiWebAppFactory fixture) : base(fixture)
    {
    }


    // Tiene un bug cuando debugeo aparece unathorized.
    [Fact]
    public async Task LoginAdmin_ok()
    {
        // Arrange
        await LoginAdmin();

        //Act
        var response = await _client.GetAsync(AuthRootUri + "/check");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // End
        await LogOut();
    }

    // El mismo bug de la linea 20 deberia aplicar para este tambien.
    [Fact]
    public async Task LoginCustomer_ok()
    {
        // Arrange
        await LoginCustomer();

        // Act
        var response = await _client.GetAsync(AuthRootUri + "/check");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        // End
        await LogOut();
    }

    [Fact]
    public async Task Login_email_does_not_exist()
    {
        // Act
        var response = await Login("wrong@wronge.com", "asdasdasd");
        var loginResult = await response.Content.ReadAsAsync<LoginResult>();
        // Assert
        response.StatusCode.Should().NotBe(HttpStatusCode.OK);

        loginResult.Succeeded.Should().BeFalse();
        loginResult.Token.Should().BeNull();
        loginResult.Errors.Should().NotBeNullOrEmpty();
        loginResult.Errors.Should().Contain("User email doesn't exist");

        await LogOut();
    }

    [Fact]
    public async Task Login_email_password_do_not_match()
    {
        // Act
        var response = await Login("admin@admin.com", "admin1234");
        var loginResult = await response.Content.ReadAsAsync<LoginResult>();
        // Assert
        response.StatusCode.Should().NotBe(HttpStatusCode.OK);

        loginResult.Succeeded.Should().BeFalse();
        loginResult.Token.Should().BeNull();
        loginResult.Errors.Should().NotBeNullOrEmpty();
        loginResult.Errors.Should().Contain("The email/password doesn't match");

        await LogOut();
    }

    [Fact]
    public async Task CreateCustomer_already_exist_email()
    {
        // Act
        var response = await _client.PostAsJsonAsync(AuthRootUri + "/register-customer", new CreateCustomerCommand
        {
            Email = "customer@customer.com",
            FirstName = "customer",
            LastName = "customer",
            Password = "customer123"
        });
        var content = await response.Content.ReadAsAsync<Result>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        content.Succeeded.Should().BeFalse();
        content.Errors.Should().Contain("Email already register");
    }

    [Fact]
    public async Task CreateCustomer_ok()
    {
        // Arrange
        var command = new CreateCustomerCommand
        {
            Email = "customer3@customer3.com",
            FirstName = "customer3",
            LastName = "customer3",
            Password = "customer123"
        };

        // Act
        var response = await _client.PostAsJsonAsync(AuthRootUri + "/register-customer", command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task CreateAdmin_already_exist_email()
    {
        // Arrange
        await LoginAdmin();
        var command = new CreateAdminCommand
        {
            Email = "admin@admin.com",
            FirstName = "admin",
            LastName = "admin",
            Password = "admin123"
        };

        // Act
        var response = await _client.PostAsJsonAsync(AuthRootUri + "/register-admin", command);
        var content = await response.Content.ReadAsAsync<Result>();

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        content.Succeeded.Should().BeFalse();
        content.Errors.Should().Contain("Email already register");

        // End
        await LogOut();
    }

    [Fact]
    public async Task CreateAdmin_unauthorized()
    {
        // Arrange
        var command = new CreateAdminCommand
        {
            Email = "admin@admin.com",
            FirstName = "admin",
            LastName = "admin",
            Password = "admin123"
        };

        // Act
        var response = await _client.PostAsJsonAsync(AuthRootUri + "/register-admin", command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task CreateAdmin_ok()
    {
        // Arrange
        await LoginAdmin();

        var command = new CreateAdminCommand
        {
            Email = "admin2@admin2.com",
            FirstName = "admin2",
            LastName = "admin2",
            Password = "admin123"
        };

        // Act
        var response = await _client.PostAsJsonAsync(AuthRootUri + "/register-admin", command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // End
        await LogOut();
    }
}

