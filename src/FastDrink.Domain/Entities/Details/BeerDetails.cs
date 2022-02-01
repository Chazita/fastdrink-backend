using FastDrink.Domain.Common;

namespace FastDrink.Domain.Entities;

public class BeerDetails : BaseDetails
{
    public float AlcoholContent { get; set; }

    public string? Style { get; set; }
}
