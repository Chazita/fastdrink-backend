using FastDrink.Domain.Common;

namespace FastDrink.Domain.Entities;

public class Category : BaseType
{
    public ICollection<Product>? Products { get; set; }
}
