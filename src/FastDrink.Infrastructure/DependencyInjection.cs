using FastDrink.Application.Common.Interfaces;
using FastDrink.Infrastructure.Persistence;
using FastDrink.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace FastDrink.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtSettings:Secret"])),
                RequireExpirationTime = false,
                ValidateAudience = true,
                ValidAudience = "https://localhost:7011",

                ValidateIssuer = true,
                ValidIssuer = "https://localhost:7011",
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("MustBeAdmin", policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, "admin");
            });

            options.AddPolicy("MustBeUser", policy =>
            {
                policy.RequireClaim(ClaimTypes.Role, "admin", "customer");
            });
        });

        return services;
    }
}
