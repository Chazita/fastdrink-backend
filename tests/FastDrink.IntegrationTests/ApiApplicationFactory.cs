using FastDrink.Application.Common.Interfaces;
using FastDrink.Infrastructure.Persistence;
using FastDrink.IntegrationTests.Mocks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using Respawn;

namespace FastDrink.IntegrationTests;

public class ApiApplicationFactory : WebApplicationFactory<Program>
{
    private readonly Checkpoint _checkpoint = new Checkpoint { WithReseed = true, DbAdapter = DbAdapter.Postgres };
    private NpgsqlConnection _connection = null!;

    public IConfiguration Configuration { get; private set; } = null!;

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var config = base.CreateHost(builder);

        using (var scope = config.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();

                var authService = services.GetRequiredService<IAuthService>();

                ApplicationDbContextSeeder.SeedDefaultUserAsync(context, authService).Wait();
                ApplicationDbContextSeeder.SeedDefaultCategoriesAsync(context).Wait();
                ApplicationDbContextSeeder.SeedDefaultContainersAsync(context).Wait();

                ApplicationDbContextSeeder.SeedDefaultBrandsAsync(context).Wait();
                ApplicationDbContextSeeder.SeedDefaultProductsAsync(context).Wait();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                logger.LogError(ex, "An error occurred while migrating or seeding the database");

                throw;
            }
        }

        return config;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();

            config.AddConfiguration(Configuration);
        });

        builder.ConfigureServices(s =>
        {
            s.AddScoped<ICloudinaryService, CloudinaryServiceMock>();
        });
    }

    public override ValueTask DisposeAsync()
    {
        _connection = new NpgsqlConnection(Configuration.GetConnectionString("DefaultConnection"));
        _connection.Open();
        _checkpoint.Reset(_connection).Wait();
        _connection.Close();
        return base.DisposeAsync();
    }
}
