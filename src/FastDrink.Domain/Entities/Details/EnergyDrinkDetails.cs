using FastDrink.Domain.Common;

namespace FastDrink.Domain.Entities;

public class EnergyDrinkDetails : BaseDetails
{
    public bool NonAlcoholic { get; set; }

    public bool Energizing { get; set; }

    public bool Dietetics { get; set; }

    public string Flavor { get; set; }
}
