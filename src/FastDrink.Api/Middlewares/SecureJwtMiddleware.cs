namespace FastDrink.Api.Middlewares;

public class SecureJwtMiddleware
{
    private readonly RequestDelegate _next;

    public SecureJwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
    {

        var token = context.Request.Cookies[configuration["CookieName"]];

        if (!string.IsNullOrEmpty(token))
        {
            context.Request.Headers.Add("Authorization", "Bearer " + token);
        }

        await _next(context);
    }
}
