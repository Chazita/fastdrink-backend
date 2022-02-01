using FastDrink.Domain.Common;

namespace FastDrink.Domain.Entities;

public class WineDetails : BaseDetails
{
    public float AlcoholContent { get; set; }

    public string? Variety { get; set; }

    public string? Style { get; set; }
}

