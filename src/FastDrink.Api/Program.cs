using FastDrink.Application.Common.Interfaces;
using FastDrink.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Api;

public class Program
{
    public async static Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();

                if (context.Database.IsSqlServer())
                {
                    context.Database.Migrate();
                }

                var authService = services.GetRequiredService<IAuthService>();

                await ApplicationDbContextSeed.SeedDefaultUserAsync(context, authService);
                await ApplicationDbContextSeed.SeedDefaultCategoriesAsync(context);
                await ApplicationDbContextSeed.SeedDefaultContainersAsync(context);
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                logger.LogError(ex, "An error occurred while migrating or seeding the database");

                throw;
            }
        }

        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                webBuilder.UseStartup<Startup>());
}