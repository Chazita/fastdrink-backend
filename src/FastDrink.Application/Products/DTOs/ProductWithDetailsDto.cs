using FastDrink.Application.Common.Mappings;
using FastDrink.Application.Products.DTOs.Details;
using FastDrink.Domain.Common;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Products.DTOs;

public class ProductWithDetailsDto : IMapFrom<Product>
{
    public string Id { get; set; } = "";

    public string Name { get; set; } = "";

    public float Price { get; set; }

    public float Volume { get; set; }

    public int Stock { get; set; }

    public float? Discount { get; set; }

    public ProductPhotoDto? Photo { get; set; }

    public BaseType? Category { get; set; }

    public BaseType? Container { get; set; }

    public BaseType? Brand { get; set; }

    public DateTime Created { get; set; }

    public DateTime? LastModified { get; set; }

    public DateTime? DeletedAt { get; set; }

    #region Details Relations
    public AlcoholDetailsDto? AlcoholDetails { get; set; }
    public BeerDetailsDto? BeerDetails { get; set; }
    public EnergyDrinkDetailsDto? EnergyDrinkDetails { get; set; }
    public FlavorDetailsDto? FlavorDetails { get; set; }
    public SodaDetailsDto? SodaDetails { get; set; }
    public WaterDetailsDto? WaterDetails { get; set; }
    public WineDetailsDto? WineDetails { get; set; }
    #endregion
}
