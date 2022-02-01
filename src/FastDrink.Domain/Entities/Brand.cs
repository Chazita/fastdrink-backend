using FastDrink.Domain.Common;

namespace FastDrink.Domain.Entities;

public class Brand : BaseType
{
    public ICollection<Product>? Products { get; set; }
}
