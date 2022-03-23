using FastDrink.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace FastDrink.Domain.Entities;

public class WineDetails : BaseDetails
{
    public float AlcoholContent { get; set; }

    [MaxLength(50)]
    public string? Variety { get; set; }

    [MaxLength(50)]
    public string? Style { get; set; }
}

