using FastDrink.Application.Common.Interfaces;
using FastDrink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace FastDrink.Infrastructure.Persistence;

public static class ApplicationDbContextSeeder
{
    public static async Task SeedDefaultUserAsync(ApplicationDbContext context, IAuthService authService)
    {
        if (!context.Role.Any())
        {

            using (var r = new StreamReader(@"Seeds/roles.json"))
            {
                var json = r.ReadToEnd();

                var roles = JsonSerializer.Deserialize<List<Role>>(json);
                if (roles != null)
                {
                    foreach (var role in roles)
                    {
                        context.Role.Add(role);
                    }
                }
            }

            await context.SaveChangesAsync();

        }

        if (!context.User.Any())
        {
            var adminRole = await context.Role.FirstAsync(x => x.Name == "admin");
            var customerRole = await context.Role.FirstAsync(x => x.Name == "customer");

            var adminSalt = authService.GenerateSalt();
            context.User.Add(new User
            {
                Email = "admin@admin.com",
                FirstName = "admin",
                LastName = "admin",
                RoleId = adminRole.Id,
                Salt = adminSalt,
                Password = authService.HashPassword("admin123", adminSalt),
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                AddressId = null,
            });

            var customerSalt = authService.GenerateSalt();
            context.User.Add(new User
            {
                Email = "customer@customer.com",
                FirstName = "customer",
                LastName = "customer",
                RoleId = customerRole.Id,
                Salt = customerSalt,
                Password = authService.HashPassword("customer123", customerSalt),
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                AddressId = null,
            });

            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedDefaultCategoriesAsync(ApplicationDbContext context)
    {
        if (!context.Category.Any())
        {
            using (var r = new StreamReader(@"Seeds/categories.json"))
            {
                var json = r.ReadToEnd();

                var categories = JsonSerializer.Deserialize<List<Category>>(json);
                if (categories != null)
                {
                    foreach (var category in categories)
                    {
                        context.Category.Add(category);
                    }
                }
            }

            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedDefaultContainersAsync(ApplicationDbContext context)
    {
        if (!context.Container.Any())
        {
            using (var r = new StreamReader(@"Seeds/containers.json"))
            {
                var json = r.ReadToEnd();

                var containers = JsonSerializer.Deserialize<List<Container>>(json);
                if (containers != null)
                {
                    foreach (var container in containers)
                    {
                        context.Container.Add(container);
                    }
                }
            }

            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedDefaultBrandsAsync(ApplicationDbContext context)
    {
        if (!context.Brands.Any())
        {
            using (var r = new StreamReader(@"Seeds/brands.json"))
            {
                var json = r.ReadToEnd();

                var brands = JsonSerializer.Deserialize<List<Brand>>(json);
                if (brands != null)
                {
                    foreach (var brand in brands)
                    {
                        context.Brands.Add(brand);
                    }
                }
            }

            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedDefaultProductsAsync(ApplicationDbContext context)
    {
        // Should seed products, product photos and product details.
        if (!context.Products.Any())
        {
            var r = new StreamReader(@"Seeds/products.json");

            var json = r.ReadToEnd();

            var products = JsonSerializer.Deserialize<List<Product>>(json);

            foreach (var product in products!)
            {
                product.Created = DateTime.Now;
                product.LastModified = DateTime.Now;
                product.LastModifiedBy = "admin@admin.com";

                product.Photo = null;
                product.AlcoholDetails = null;
                product.BeerDetails = null;
                product.EnergyDrinkDetails = null;
                product.FlavorDetails = null;
                product.SodaDetails = null;
                product.WaterDetails = null;
                product.WineDetails = null;

                context.Products.Add(product);
            }

            await context.SaveChangesAsync();

            products = JsonSerializer.Deserialize<List<Product>>(json);

            foreach (var product in products!)
            {
                switch (product.CategoryId)
                {
                    case 1:
                        context.BeerDetails.Add(product.BeerDetails!);
                        break;

                    case 2:
                        context.AlcoholDetails.Add(product.AlcoholDetails!);
                        break;

                    case 3:
                        context.EnergyDrinkDetails.Add(product.EnergyDrinkDetails!);
                        break;

                    case 4:
                        context.SodaDetails.Add(product.SodaDetails!);
                        break;

                    case 5:
                        context.WaterDetails.Add(product.WaterDetails!);
                        break;

                    case 6:
                        context.WineDetails.Add(product.WineDetails!);
                        break;

                    case 7:
                        context.FlavorDetails.Add(product.FlavorDetails!);
                        break;

                    case 8:
                        context.FlavorDetails.Add(product.FlavorDetails!);
                        break;
                    default:
                        break;
                }
                context.ProductPhoto.Add(product.Photo!);
            }

            await context.SaveChangesAsync();

            r.Close();
        }
    }
}
