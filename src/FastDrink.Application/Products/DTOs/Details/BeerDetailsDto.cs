using FastDrink.Application.Common.Mappings;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Products.DTOs.Details;

public class BeerDetailsDto : IMapFrom<BeerDetails>
{
    public string ProductId { get; set; } = string.Empty;

    public float AlcoholContent { get; set; }

    public string? Style { get; set; } = string.Empty;
}
