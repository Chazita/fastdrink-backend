using FastDrink.Domain.Common;

namespace FastDrink.Domain.Entities;

public class Role : BaseType
{
    public IList<User>? Users { get; set; }
}
