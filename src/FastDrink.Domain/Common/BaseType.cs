using System.ComponentModel.DataAnnotations;

namespace FastDrink.Domain.Common;

public abstract class BaseType
{
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; }
}
