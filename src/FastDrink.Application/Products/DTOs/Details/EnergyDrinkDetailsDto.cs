using FastDrink.Application.Common.Mappings;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Products.DTOs.Details;

public class EnergyDrinkDetailsDto : IMapFrom<EnergyDrinkDetails>
{
    public string ProductId { get; set; } = string.Empty;

    public bool NonAlcoholic { get; set; }

    public bool Energizing { get; set; }

    public bool Dietetics { get; set; }

    public string Flavor { get; set; } = string.Empty;
}
