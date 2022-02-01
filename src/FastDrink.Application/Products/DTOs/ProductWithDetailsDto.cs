using FastDrink.Application.Common.Mappings;
using FastDrink.Domain.Common;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Products.DTOs;

public class ProductWithDetailsDto : IMapFrom<Product>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public float Price { get; set; }

    public float Volumen { get; set; }

    public int Stock { get; set; }

    public float? Discount { get; set; }

    public ICollection<ProductPhotoDto>? Photos { get; set; }

    public BaseType? Category { get; set; }

    public BaseType? Container { get; set; }

    public BaseType? Brand { get; set; }

    public DateTime Created { get; set; }

    public DateTime? LastModified { get; set; }

    public DateTime? DeletedAt { get; set; }

    #region Details Relations
    public AlcoholDetails? AlcoholDetails { get; set; }
    public BeerDetails? BeerDetails { get; set; }
    public EnergyDrinkDetails? EnergyDrinkDetails { get; set; }
    public FlavorDetails? FlavorDetails { get; set; }
    public SodaDetails? SodaDetails { get; set; }
    public WaterDetails? WaterDetails { get; set; }
    public WineDetails? WineDetails { get; set; }
    #endregion
}
