using FastDrink.Application.Common.Mappings;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Products.DTOs.Details;

public class SodaDetailsDto : IMapFrom<SodaDetails>
{
    public string ProductId { get; set; }

    public string Flavor { get; set; }

    public bool Dietetics { get; set; }
}
