using Newtonsoft.Json;

namespace FastDrink.IntegrationTests;

public static class HttpContentExtension
{
    public static async Task<TResponse> DeserializeContent<TResponse>(this HttpContent httpContent)
    {
        var content = await httpContent.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(content);
    }
}
