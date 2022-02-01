using FastDrink.Application.Common.Interfaces;
using FastDrink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedDefaultUserAsync(ApplicationDbContext context, IAuthService authService)
    {
        if (!context.Role.Any())
        {
            context.Role.Add(new Role
            {
                Name = "admin"
            });

            context.Role.Add(new Role
            {
                Name = "customer"
            });

            await context.SaveChangesAsync();
        }

        if (!context.User.Any())
        {
            var adminRole = context.Role.FirstAsync(x => x.Name == "admin");
            var customerRole = context.Role.FirstAsync(x => x.Name == "customer");

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
            context.Category.Add(new Category
            {
                Name = "cerveza"
            });

            context.Category.Add(new Category
            {
                Name = "alcohol"
            });

            context.Category.Add(new Category
            {
                Name = "bebida_energizante"
            });

            context.Category.Add(new Category
            {
                Name = "gaseosa"
            });

            context.Category.Add(new Category
            {
                Name = "agua"
            });

            context.Category.Add(new Category
            {
                Name = "vino"
            });

            context.Category.Add(new Category
            {
                Name = "jugo"
            });

            context.Category.Add(new Category
            {
                Name = "bebida_isotónica"
            });

            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedDefaultContainersAsync(ApplicationDbContext context)
    {
        if (!context.Container.Any())
        {
            context.Container.Add(new Container
            {
                Name = "botella"
            });

            context.Container.Add(new Container
            {
                Name = "lata"
            });

            context.Container.Add(new Container
            {
                Name = "bidon"
            });

            await context.SaveChangesAsync();
        }
    }
}
