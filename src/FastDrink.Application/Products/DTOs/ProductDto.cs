using FastDrink.Application.Common.Mappings;
using FastDrink.Domain.Common;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Products.DTOs;

public class ProductDto : IMapFrom<Product>
{
    public string Id { get; set; } = "";

    public string Name { get; set; } = "";

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
}
