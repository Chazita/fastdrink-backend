using FastDrink.Application.Common.Interfaces;
using FastDrink.Domain.Common;
using FastDrink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FastDrink.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> User => Set<User>();

    public DbSet<Role> Role => Set<Role>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Container> Container => Set<Container>();

    public DbSet<Category> Category => Set<Category>();

    public DbSet<Brand> Brands => Set<Brand>();

    public DbSet<AlcoholDetails> AlcoholDetails => Set<AlcoholDetails>();

    public DbSet<BeerDetails> BeerDetails => Set<BeerDetails>();

    public DbSet<EnergyDrinkDetails> EnergyDrinkDetails => Set<EnergyDrinkDetails>();

    public DbSet<FlavorDetails> FlavorDetails => Set<FlavorDetails>();

    public DbSet<SodaDetails> SodaDetails => Set<SodaDetails>();

    public DbSet<WaterDetails> WaterDetails => Set<WaterDetails>();

    public DbSet<WineDetails> WineDetails => Set<WineDetails>();

    public DbSet<Address> Address => Set<Address>();

    public DbSet<Order> Order => Set<Order>();

    public DbSet<OrderProduct> OrderProduct => Set<OrderProduct>();

    public DbSet<ProductPhoto> ProductPhoto => Set<ProductPhoto>();

    public DbSet<T> GetDbSetDetails<T>() where T : BaseDetails
    {
        return Set<T>();
    }

    public DbSet<T> GetDbSetType<T>() where T : BaseType
    {
        return Set<T>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
