using FastDrink.Domain.Common;
using FastDrink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> User { get; }
    DbSet<Role> Role { get; }
    DbSet<Address> Address { get; }
    DbSet<Order> Order { get; }
    DbSet<OrderProduct> OrderProduct { get; }
    DbSet<Product> Products { get; }
    DbSet<ProductPhoto> ProductPhoto { get; }
    DbSet<Container> Container { get; }
    DbSet<Category> Category { get; }
    DbSet<Brand> Brands { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    DbSet<T> GetDbSetType<T>() where T : BaseType;
    DbSet<T> GetDbSetDetails<T>() where T : BaseDetails;

    #region Details Db
    DbSet<AlcoholDetails> AlcoholDetails { get; }
    DbSet<BeerDetails> BeerDetails { get; }
    DbSet<EnergyDrinkDetails> EnergyDrinkDetails { get; }
    DbSet<FlavorDetails> FlavorDetails { get; }
    DbSet<SodaDetails> SodaDetails { get; }
    DbSet<WaterDetails> WaterDetails { get; }
    DbSet<WineDetails> WineDetails { get; }
    #endregion
}
