using FastDrink.Api;
using FastDrink.Application.Common.Interfaces;
using FastDrink.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace FastDrink.IntegrationTests.Fixtures;

public class ApiWebAppFactory : WebApplicationFactory<Startup>
{
    public IConfiguration Configuration { get; private set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d =>
                            d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            services.Remove(descriptor);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDb");
            });
        });
    }

    // Esto deberia estar causando el bug del test.
    // Posible solucion implementar lo mismo pero el configureServices.
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var host = base.CreateHost(builder);
        using (var scope = host.Services.CreateScope())
        {
            var s = scope.ServiceProvider;

            try
            {
                var context = s.GetRequiredService<ApplicationDbContext>();

                if (context.Database.IsSqlServer())
                {
                    context.Database.Migrate();
                }

                var authService = s.GetRequiredService<IAuthService>();

                ApplicationDbContextSeed.SeedDefaultUserAsync(context, authService).Wait();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                logger.LogError(ex, "An error occurred while migrating or seeding the database");

                throw;
            }
        }

        return host;
    }
}
