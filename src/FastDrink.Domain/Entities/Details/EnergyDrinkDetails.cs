using FastDrink.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace FastDrink.Domain.Entities;

public class EnergyDrinkDetails : BaseDetails
{
    public bool NonAlcoholic { get; set; }

    public bool Energizing { get; set; }

    public bool Dietetics { get; set; }

    [MaxLength(50)]
    public string Flavor { get; set; } = string.Empty;
}
