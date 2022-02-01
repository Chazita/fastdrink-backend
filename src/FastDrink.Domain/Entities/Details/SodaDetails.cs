using FastDrink.Domain.Common;

namespace FastDrink.Domain.Entities;

public class SodaDetails : BaseDetails
{
    public string Flavored { get; set; }

    public bool Dietetics { get; set; }
}
