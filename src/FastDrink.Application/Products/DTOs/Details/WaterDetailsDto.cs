using FastDrink.Application.Common.Mappings;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Products.DTOs.Details;

public class WaterDetailsDto : IMapFrom<WaterDetails>
{
    public string ProductId { get; set; }

    public bool LowInSodium { get; set; }

    public bool Gasified { get; set; }
}
