using FastDrink.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace FastDrink.Domain.Entities;

public class Product : AuditableEntity
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public float Price { get; set; }

    [Required]
    public float Volumen { get; set; }

    [Required]
    public int Stock { get; set; }

    public float? Discount { get; set; }

    public ICollection<ProductPhoto>? Photos { get; set; }

    public ICollection<OrderProduct>? Orders { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    [Required]
    public int ContainerId { get; set; }
    public Container? Container { get; set; }

    [Required]
    public int BrandId { get; set; }
    public Brand? Brand { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string LastModifiedBy { get; set; }

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
