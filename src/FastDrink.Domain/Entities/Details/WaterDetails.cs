using FastDrink.Domain.Common;

namespace FastDrink.Domain.Entities;

public class WaterDetails : BaseDetails
{
    public bool LowInSodium { get; set; }

    public bool Gasified { get; set; }
}
