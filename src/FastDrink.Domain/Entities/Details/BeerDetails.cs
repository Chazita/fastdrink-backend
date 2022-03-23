using FastDrink.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace FastDrink.Domain.Entities;

public class BeerDetails : BaseDetails
{
    public float AlcoholContent { get; set; }

    [MaxLength(50)]
    public string? Style { get; set; }
}
