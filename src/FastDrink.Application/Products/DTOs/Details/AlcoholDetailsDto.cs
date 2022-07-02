using FastDrink.Application.Common.Mappings;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Products.DTOs.Details;

public class AlcoholDetailsDto : IMapFrom<AlcoholDetails>
{
    public string ProductId { get; set; } = string.Empty;
    public float AlcoholContent { get; set; }
}
