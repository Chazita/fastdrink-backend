using FastDrink.Application.Common.Mappings;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Products.DTOs.Details;

public class FlavorDetailsDto : IMapFrom<FlavorDetails>
{
    public string ProductId { get; set; } = string.Empty;

    public string Flavor { get; set; } = string.Empty;
}
