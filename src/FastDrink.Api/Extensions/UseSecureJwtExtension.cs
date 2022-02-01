using FastDrink.Api.Middlewares;

namespace FastDrink.Api.Extensions;

public static class UseSecureJwtExtension
{
    public static IApplicationBuilder UseSecureJwt(this IApplicationBuilder builder) => builder.UseMiddleware<SecureJwtMiddleware>();
}
