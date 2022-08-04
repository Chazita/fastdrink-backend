using FastDrink.Application.Auth.Commands.Login;
using Newtonsoft.Json;
using System.Text;

namespace FastDrink.IntegrationTests;

public class IntegrationTest : IClassFixture<ApiApplicationFactory>
{
    protected readonly ApiApplicationFactory _factory;
    protected readonly HttpClient _client;

    public IntegrationTest(ApiApplicationFactory factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    protected async Task LoginAuth(string email, string password)
    {
        var loginCommand = new LoginCommand
        {
            Email = email,
            Password = password
        };

        var json = JsonConvert.SerializeObject(loginCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/Auth/login", data);

        if (!response.IsSuccessStatusCode)
        {
            var loginResult = await response.Content.DeserializeContent<LoginResult>();
            throw new Exception("Not valid user");
        }

        var value = response.Headers.First(x => x.Key == "Set-Cookie").Value.First();

        if (value == null)
        {
            throw new Exception("No existe tal cookie");
        }

        _client.DefaultRequestHeaders.Add("Cookie", value);
    }

    protected static MemoryStream CreateFormFile()
    {
        var bytes = Encoding.UTF8.GetBytes("this is dummy");
        var file = new MemoryStream(bytes);

        return file;
    }
}
