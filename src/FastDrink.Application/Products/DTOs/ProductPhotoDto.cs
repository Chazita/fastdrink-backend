using FastDrink.Application.Common.Mappings;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Products.DTOs;

public class ProductPhotoDto : IMapFrom<ProductPhoto>
{
    public int Id { get; set; }

    public string PhotoUrl { get; set; } = string.Empty;
}
