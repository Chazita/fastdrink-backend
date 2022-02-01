using FastDrink.Application.Auth.Commands;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace FastDrink.IntegrationTests.Fixtures;

public class IntegrationTest : IClassFixture<ApiWebAppFactory>
{
    protected readonly ApiWebAppFactory _factory;
    protected readonly HttpClient _client;

    public IntegrationTest(ApiWebAppFactory fixture)
    {
        _factory = fixture;
        _client = _factory.CreateClient();
    }

    #region Login Methods
    protected async Task LoginAdmin()
    {
        await _client.PostAsJsonAsync("/api/Auth/login", new LoginCommand
        {
            Email = "admin@admin.com",
            Password = "admin123"
        });
    }

    protected async Task LoginCustomer()
    {
        await _client.PostAsJsonAsync("/api/Auth/login", new LoginCommand
        {
            Email = "customer@customer.com",
            Password = "customer123"
        });
    }

    protected async Task LogOut()
    {
        await _client.GetAsync("/api/Auth/log-out");
    }

    protected async Task<HttpResponseMessage> Login(string email, string password)
    {
        return await _client.PostAsJsonAsync("/api/Auth/login", new LoginCommand
        {
            Email = email,
            Password = password
        });
    }
    #endregion
}
