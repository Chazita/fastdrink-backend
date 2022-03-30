using FastDrink.Application.Common.Mappings;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Products.DTOs.Details;

public class WineDetailsDto : IMapFrom<WineDetails>
{
    public string ProductId { get; set; }

    public float AlcoholContent { get; set; }

    public string? Variety { get; set; }

    public string? Style { get; set; }
}
