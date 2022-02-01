using FastDrink.Domain.Common;

namespace FastDrink.Domain.Entities;

public class Container : BaseType
{
    public ICollection<Product>? Products { get; set; }
}
