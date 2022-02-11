using FastDrink.Domain.Common;

namespace FastDrink.Domain.Entities;

public class SodaDetails : BaseDetails
{
    public string Flavor { get; set; }

    public bool Dietetics { get; set; }
}
