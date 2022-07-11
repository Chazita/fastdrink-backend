using FastDrink.Application.Common.Interfaces;
using FastDrink.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Api;

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = new ConfigurationBuilder().AddCommandLine(args);
        var config = builder.Build();
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();

                context.Database.Migrate();

                var authService = services.GetRequiredService<IAuthService>();

                await ApplicationDbContextSeeder.SeedDefaultUserAsync(context, authService);
                await ApplicationDbContextSeeder.SeedDefaultCategoriesAsync(context);
                await ApplicationDbContextSeeder.SeedDefaultContainersAsync(context);

                if (config.GetSection("AddProduct").Value != null && bool.Parse(config.GetSection("AddProduct").Value))
                {
                    await ApplicationDbContextSeeder.SeedDefaultBrandsAsync(context);
                    await ApplicationDbContextSeeder.SeedDefaultProductsAsync(context);
                }
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